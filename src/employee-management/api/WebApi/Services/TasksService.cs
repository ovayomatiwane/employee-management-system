using Microsoft.EntityFrameworkCore;
using WebApi.CustomExceptions;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Services.Interfaces;
using TaskEntity = WebApi.Models.Entities.Task;
using Task = System.Threading.Tasks.Task;
using WebApi.Dtos.QueryData;
using WebApi.Models.Entities;

namespace WebApi.Services
{
    public class TasksService(EmployeeManagementDataContext databaseContext) : ITasksService
    {
        public async Task CreateTaskAsync(TaskDto task, CancellationToken cancellationToken = default)
        {
            ValidateTaskDto(task);

            TaskEntity taskEntity = new()
            {
                Id = Guid.NewGuid(),
                TaskName = task.TaskName,
                Description = task.Description,
                MaxDuration = task.MaxDuration,
                CreatedDate = DateTime.UtcNow,
            };

            databaseContext.Add(taskEntity);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskDto>> GetByConsultantIdAsync(Guid consultantId, CancellationToken cancellationToken = default)
        {
            var consultant = await databaseContext.Consultants
                                                  .Include(x => x.ConsultantTasks)
                                                    .ThenInclude(y => y.Task)
                                                  .FirstOrDefaultAsync(x => x.Id == consultantId, cancellationToken);
            if (consultant is null)
            {
                string message = $"The consultant with ID: {consultantId} was not found.";
                throw new EntityNotFoundException(message);
            }

            if (consultant.ConsultantTasks.Any())
            {
                return consultant.ConsultantTasks.Select(x => x.Task.MapToDto());
            }

            return [];
        }

        public async Task<TaskDto> GetTaskAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            var task = await databaseContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);

            if (task is null)
            {
                string message = $"The task with ID: {taskId} was not found.";
                throw new EntityNotFoundException(message);
            }

            return task.MapToDto();
        }

        public async Task RemoveTaskAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            //When removing a task, we mark it as done
            var task = await databaseContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);

            if (task is null)
            {
                if (task is null)
                {
                    string message = $"Task with Id: {taskId} not found";
                    throw new EntityNotFoundException(message);
                }
            }

            task.CompletionDate = DateTime.UtcNow;

            databaseContext.Tasks.Update(task);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTaskAsync(TaskDto taskDto, CancellationToken cancellationToken = default)
        {
            ValidateTaskDto(taskDto);

            var task = await databaseContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskDto.Id, cancellationToken);

            if (task is null)
            {
                string message = $"Task with Id: {taskDto.Id} not found";
                throw new EntityNotFoundException(message);
            }
            

            task.TaskName = taskDto.TaskName;
            task.Description = taskDto.Description;
            task.MaxDuration = taskDto.MaxDuration;
            task.CreatedDate = taskDto.CreatedDate;

            databaseContext.Update(task);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default)
        {
            var tasks = await databaseContext.Tasks.ToListAsync(cancellationToken);

            return tasks.MapToDtos();
        }

        private void ValidateTaskDto(TaskDto task)
        {
            if (task.MaxDuration <= 0)
            {
                string message = $"maximum duration of a task has to be greater than an hour";
                throw new Exception(message);
            }
        }

        public async Task<IEnumerable<TaskDto>> GetUnassignedAsync(CancellationToken cancellationToken = default)
        {
            var unassignedTasks = await databaseContext.Tasks
                                                       .Where(x => !databaseContext.ConsultantTasks.Any(y => x.Id == y.TaskId))
                                                       .ToListAsync();

            return unassignedTasks.MapToDtos();
        }

        public async Task<TaskDto> CompleteAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            DateTime timeCompletion = DateTime.UtcNow;

            var taskEntity = await databaseContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId && x.CompletionDate == null);

            if (taskEntity is null)
            {
                string message = $"Task with Id: {taskId} is either completed or does not exist";
                throw new Exception(message);
            }

            var consultantTasks = await databaseContext.ConsultantTasks
                                                       .Include(x => x.RoleRate)
                                                       .Where(x => x.TaskId == taskId)
                                                       .ToListAsync(cancellationToken);

            taskEntity.CompletionDate = timeCompletion;

            foreach(var consultantTask  in consultantTasks)
            {
                // Complete each and update
                consultantTask.HoursCompleted = consultantTask.AssignedHours;
                consultantTask.RoleRate.EndDate = timeCompletion;
            }

            databaseContext.Tasks.Update(taskEntity);
            databaseContext.ConsultantTasks.UpdateRange(consultantTasks);

            await databaseContext.SaveChangesAsync(cancellationToken);

            return taskEntity.MapToDto();
        }

        public async Task<ConsultantTaskDto> CompleteTaskHoursAsync(ConsultantTaskHoursDto completedDto, CancellationToken cancellationToken = default)
        {
            var consultantTask = await databaseContext.ConsultantTasks
                                                      .FirstOrDefaultAsync(x => x.Id == completedDto.ConsultantTaskId, cancellationToken);

            string message;
            if (consultantTask is null)
            {
                message = $"Consultant Task with Id: {completedDto.ConsultantTaskId} not found";
                throw new EntityNotFoundException(message);
            }

            if (consultantTask.HoursCompleted >= consultantTask.AssignedHours)
            {
                message = $"Maximum number of hours already completed on the ConsultantTask with Id: {completedDto.ConsultantTaskId}";
                throw new Exception(message);
            }

            var totalHours = completedDto.CompletedHours > consultantTask.AssignedHours ? consultantTask.AssignedHours : completedDto.CompletedHours;

            consultantTask.HoursCompleted = totalHours;
            databaseContext.ConsultantTasks.UpdateRange(consultantTask);

            await databaseContext.SaveChangesAsync(cancellationToken);

            var updatedConsultantTask = await databaseContext.ConsultantTasks
                                                      .Include(x => x.Task)
                                                      .Include(x => x.RoleRate)
                                                        .ThenInclude(y => y.Role)
                                                      .Include(x => x.Consultant)
                                                      .FirstOrDefaultAsync(x => x.Id == completedDto.ConsultantTaskId, cancellationToken);

            return updatedConsultantTask.MapToDto();
        }
    }
}
