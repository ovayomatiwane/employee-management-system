using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RolesService : IRolesService
    {
        public async Task CreateRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoleAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<RoleDto> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
