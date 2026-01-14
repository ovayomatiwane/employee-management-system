
using WebApi.Dtos;
using WebApi.Dtos.QueryData;

namespace WebApi.Services.Interfaces
{
    public interface IRoleRatesService
    {
        Task<ConsultantTaskDto> AssignTaskToConsultantAsync(AssignConsultantDto assignConsultant, CancellationToken cancellationToken = default);

        Task<RoleRateDto> CreateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default);

        Task<IEnumerable<RoleRateDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<RoleRateDto> UpdateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default);
    }
}
