using WebApi.Dtos;
using WebApi.Dtos.QueryData;
using TaskEntity = WebApi.Models.Entities.Task;

namespace WebApi.Services.Interfaces
{
    public interface ITasksService
    {
        Task CreateTaskAsync(TaskDto task, CancellationToken cancellationToken = default);

        Task RemoveTaskAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task UpdateTaskAsync(TaskDto task, CancellationToken cancellationToken = default);

        Task<TaskDto> GetTaskAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task<IEnumerable<TaskDto>> GetAllTasksAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<TaskDto>> GetByConsultantIdAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task<IEnumerable<TaskDto>> GetUnassignedAsync(CancellationToken cancellationToken = default);

        Task<TaskDto> CompleteAsync(Guid taskId, CancellationToken cancellationToken = default);

        Task<ConsultantTaskDto> CompleteTaskHoursAsync(ConsultantTaskHoursDto completedDto, CancellationToken cancellationToken = default);
    }
}
