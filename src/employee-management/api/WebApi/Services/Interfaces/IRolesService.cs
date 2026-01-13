using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IRolesService
    {
        Task CreateRoleAsync(RoleDto role, CancellationToken cancellationToken = default);

        Task RemoveRoleAsync(Guid roleId, CancellationToken cancellationToken = default);

        Task UpdateRoleAsync(RoleDto role, CancellationToken cancellationToken = default);

        Task<RoleDto> GetRoleAsync(Guid roleId, CancellationToken cancellationToken= default);

        Task<IEnumerable<RoleDto>> GetAllRoleAsync(CancellationToken cancellationToken = default);
    }
}
