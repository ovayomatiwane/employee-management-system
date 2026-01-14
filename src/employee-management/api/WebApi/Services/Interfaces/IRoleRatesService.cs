
using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IRoleRatesService
    {
        Task<IEnumerable<RoleRateDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
