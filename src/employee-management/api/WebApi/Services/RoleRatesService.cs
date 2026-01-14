using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RoleRatesService(EmployeeManagementDataContext databaseContext) : IRoleRatesService
    {
        public async Task<IEnumerable<RoleRateDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var roleRates = await databaseContext.RoleRates.ToListAsync(cancellationToken);

            return roleRates.MapToDtos();
        }
    }
}
