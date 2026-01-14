using Microsoft.EntityFrameworkCore;
using System.Threading;
using WebApi.CustomExceptions;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WebApi.Services
{
    public class RoleRatesService(EmployeeManagementDataContext databaseContext) : IRoleRatesService
    {
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
    }
}
