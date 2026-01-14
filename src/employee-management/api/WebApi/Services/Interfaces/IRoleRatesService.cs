
using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IRoleRatesService
    {
        Task<IEnumerable<RoleRateDto>> GetAll(CancellationToken cancellationToken = default);
    }
}
