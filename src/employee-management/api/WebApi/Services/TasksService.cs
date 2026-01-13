using WebApi.Dtos;
using WebApi.Services.Interfaces;
using TaskEntity = WebApi.Models.Entities.Task;

namespace WebApi.Services
{
    public class TasksService() : ITasksService
    {
        public async Task CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskDto> GetAllTasksAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskDto> GetTaskAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveTaskAsync(Guid taskId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTaskAsync(TaskEntity task, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
