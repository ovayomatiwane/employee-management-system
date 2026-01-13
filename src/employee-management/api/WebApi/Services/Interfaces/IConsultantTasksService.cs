using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IConsultantTasksService
    {
        Task CreateConsultantTaskAsync(ConsultantTaskDto consultantTask, CancellationToken cancellationToken = default);

        Task RemoveConsultantTaskAsync(Guid consultantTaskId, CancellationToken cancellationToken = default);

        Task UpdateConsultantTaskAsync(ConsultantTaskDto consultantTask, CancellationToken cancellationToken = default);

        Task<ConsultantTaskDto> GetConsultantTaskAsync(Guid consultantTaskId, CancellationToken cancellationToken = default);

        Task<IEnumerable<ConsultantTaskDto>> GetAllConsultantTasksAsync(CancellationToken cancellationToken = default);
    }
}
