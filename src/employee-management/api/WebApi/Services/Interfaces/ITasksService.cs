using WebApi.Dtos;
using TaskEntity = WebApi.Models.Entities.Task;

namespace WebApi.Services.Interfaces
{
    public interface ITasksService
    {
        Task CreateTaskAsync(TaskEntity task, CancellationToken cancellationToken = default);

        Task RemoveTaskAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task UpdateTaskAsync(TaskEntity task, CancellationToken cancellationToken = default);

        Task<TaskDto> GetTaskAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task<TaskDto> GetAllTasksAsync(CancellationToken cancellationToken = default);
    }
}
