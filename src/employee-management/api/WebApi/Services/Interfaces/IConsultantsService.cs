using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IConsultantsService
    {
        Task CreateConsultantAsync(ConsultantDto consultant, CancellationToken cancellationToken = default);

        Task RemoveConsultantAsync(Guid consultantId, CancellationToken cancellationToken = default);

        Task UpdateConsultantAsync(ConsultantDto consultant, CancellationToken cancellationToken = default);

        Task<ConsultantDto> GetConsultantAsync(Guid consultantId, CancellationToken cancellationToken = default);

        Task<IEnumerable<ConsultantDto>> GetAllConsultantsAsync(CancellationToken cancellationToken = default);
    }
}
