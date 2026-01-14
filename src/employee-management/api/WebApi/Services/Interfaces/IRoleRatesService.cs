
using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IRoleRatesService
    {
        Task<RoleRateDto> CreateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default);

        Task<IEnumerable<RoleRateDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<RoleRateDto> UpdateRoleRates(RoleRateDto roleRate, CancellationToken cancellationToken = default);
    }
}
