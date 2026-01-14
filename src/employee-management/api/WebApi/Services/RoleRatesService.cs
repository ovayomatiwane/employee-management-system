using Microsoft.EntityFrameworkCore;
using System.Threading;
using WebApi.CustomExceptions;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Dtos.QueryData;
using WebApi.mapping;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using WebApi.Utils;
using Task = System.Threading.Tasks.Task;

namespace WebApi.Services
{
    public class RoleRatesService(EmployeeManagementDataContext databaseContext) : IRoleRatesService
    {

        private readonly int MAX_DAILY_HOURS = 12;

        public async Task<RoleRateDto> CreateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default)
        {
            ValidateRoleRateDto(roleRate);
            var role = await GetRole(roleRate.RoleId, cancellationToken);

            DateTime startDate = DateTime.UtcNow;

            RoleRate roleRateEntity = new()
            {
                Id = Guid.NewGuid(),
                HourlyRate = roleRate.HourlyRate,
                RoleId = role.Id,
                StartDate = startDate,
                AssignedDate = startDate,
            };

            databaseContext.RoleRates.Add(roleRateEntity);

            await databaseContext.SaveChangesAsync(cancellationToken);

            return roleRateEntity.MapToDto();
        }

        public async Task<IEnumerable<RoleRateDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var roleRates = await databaseContext.RoleRates.ToListAsync(cancellationToken);

            return roleRates.MapToDtos();
        }

        public async Task<RoleRateDto> UpdateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default)
        {
            ValidateRoleRateDto(roleRate);
            var role = await GetRole(roleRate.RoleId, cancellationToken);
            
            DateTime dateNow = DateTime.UtcNow;

            var oldRate = await GetById(roleRate.Id, cancellationToken);

            oldRate.EndDate = dateNow;
            databaseContext.RoleRates.Update(oldRate);

            await databaseContext.SaveChangesAsync(cancellationToken);

            RoleRateDto newRoleRate = new()
            {
                Id = Guid.NewGuid(),
                HourlyRate = roleRate.HourlyRate,
                RoleId = role.Id,
                AssignedDate = dateNow,
                StartDate = dateNow,
            };

            return await CreateRoleRates(newRoleRate);
        }

        private async Task<RoleDto> GetRole(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);

            if (role is null)
            {
                string message = $"Role with role Id {roleId} not found.";
                throw new EntityNotFoundException(message);
            }

            return role.MapToDto();
        }

        private async Task<RoleRate> GetById(Guid id, CancellationToken cancellationToken)
        {
            var roleRate = await databaseContext.RoleRates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (roleRate is null)
            {
                string message = $"Role rate with Id {id} not found.";
                throw new EntityNotFoundException(message);
            }

            return roleRate;
        }

        private void ValidateRoleRateDto(RoleRateDto roleRate)
        {
            string message;

            if (roleRate is null)
            {
                message = $"Argument {nameof(roleRate)} cannot be null.";
                throw new ArgumentNullException(message);
            }

            if (roleRate.HourlyRate <= 0)
            {
                message = $"{nameof(roleRate.HourlyRate)} must be greater than zero.";
                throw new ArgumentOutOfRangeException(message);
            }

            if (roleRate.RoleId == Guid.Empty)
            {
                message = $"Role rate must be assigned to an existing role: {nameof(roleRate.RoleId)}.";
                throw new ArgumentOutOfRangeException(message);
            }
        }

        public async Task<ConsultantTaskDto> AssignTaskToConsultantAsync(AssignConsultantDto assignConsultant, CancellationToken cancellationToken = default)
        {
            //TODO: Validate the DTO
            DateTime datenow = DateTime.UtcNow;
            string message;

            var consultant = await databaseContext.Consultants.FirstOrDefaultAsync(x => x.Id == assignConsultant.ConsultantId, cancellationToken);
            if (consultant is null)
            {
                message = $"consultant with Id: {assignConsultant.ConsultantId} not found. ";
                throw new EntityNotFoundException(message);
            }

            var task = await databaseContext.Tasks.FirstOrDefaultAsync(x => x.Id == assignConsultant.TaskId, cancellationToken);
            if (task is null)
            {
                message = $"task with Id: {assignConsultant.TaskId} not found. ";
                throw new EntityNotFoundException(message);
            }

            var role = await databaseContext.Roles.FirstOrDefaultAsync(x => x.Id == assignConsultant.RoleId || x.Name == assignConsultant.RoleName, cancellationToken);
            if (role is null)
            {
                message = $"role with Id: {assignConsultant.RoleId} or role with name {assignConsultant.RoleName} not found. ";
                throw new EntityNotFoundException(message);
            }

            //Check if they do not have too many tasks on the day
            DateTime startOfday = datenow.StartOfDay();
            DateTime endOfDay = datenow.EndOfDay();

            var assignedHours = 0;

            var assignedTasks = await databaseContext.ConsultantTasks.Include(x => x.Task)
                                                                     .Where(x  => x.AssignedDate <= endOfDay 
                                                                                  && x.AssignedDate > startOfday
                                                                                  && x.ConsultantId == assignConsultant.ConsultantId)
                                                                     .ToListAsync(cancellationToken);

            foreach(var assignedTask  in assignedTasks)
            {
                assignedHours += assignedTask.AssignedHours;
            }

            var multipleAssignedTasks = await databaseContext.ConsultantTasks.Include(x => x.Task)
                                                                     .Where(x => x.AssignedDate <= endOfDay
                                                                                  && x.AssignedDate > startOfday
                                                                                  && x.TaskId == assignConsultant.TaskId)
                                                                     .ToListAsync(cancellationToken);
            var totalAssignedTaskTime = 0;
            foreach(var assignedTask in multipleAssignedTasks)
            {
                totalAssignedTaskTime += assignedTask.AssignedHours;
            }

            var availableHours = MAX_DAILY_HOURS - assignedHours;
            var timeToAssign = task.MaxDuration - totalAssignedTaskTime;


            if (availableHours < timeToAssign)
            {
                message = $"Consultant with Id: {assignConsultant.ConsultantId} does not have enough available time. Available: {availableHours}, Time to assign: {timeToAssign}";
                throw new Exception(message);
            }

            Guid roleRateId = Guid.NewGuid();
            RoleRate roleRate = new()
            {
                Id = roleRateId,
                HourlyRate = assignConsultant.HourlyRate,
                RoleId = role.Id,
                AssignedDate = datenow,
                StartDate = datenow,
            };

            databaseContext.RoleRates.Add(roleRate);

            Guid consultantTaskId = Guid.NewGuid();

            ConsultantTask consultantTask = new()
            {
                Id = consultantTaskId,
                TaskId = task.Id,
                ConsultantId = consultant.Id,
                AssignedDate = datenow,
                HoursCompleted = 0,
                AssignedHours = timeToAssign,
                RoleRateId = roleRateId
            };

            databaseContext.ConsultantTasks.Add(consultantTask);

            var result = await databaseContext.SaveChangesAsync(cancellationToken);

            var createdEntity = await databaseContext.ConsultantTasks.FirstOrDefaultAsync(x => x.Id == consultantTaskId);

            return createdEntity.MapToDto();
        }
    }
}
