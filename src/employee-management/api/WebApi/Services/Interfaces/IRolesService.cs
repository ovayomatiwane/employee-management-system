using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IRolesService
    {
        Task<RoleDto> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);

        Task<RoleDto> GetRoleAsync(Guid roleId, CancellationToken cancellationToken= default);

        Task<IEnumerable<RoleDto>> GetAllRoleAsync(CancellationToken cancellationToken = default);
    }
}
