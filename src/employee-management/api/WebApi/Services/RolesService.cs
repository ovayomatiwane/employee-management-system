using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RolesService(EmployeeManagementDataContext databaseContext) : IRolesService
    {
        public async Task<IEnumerable<RoleDto>> GetAllRoleAsync(CancellationToken cancellationToken = default)
        {
            var roles = await databaseContext.Roles.ToListAsync(cancellationToken);

            return roles.MapToDtos();
        }

        public async Task<RoleDto> GetByNameAsync(string roleName, CancellationToken cancellationToken = default)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);

            if (role is null)
            {
                return new() 
                { 
                    Id = Guid.Empty,
                    Name = string.Empty
                };
            }

            return role.MapToDto();
        }

        public async Task<RoleDto> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);

            if (role is null)
            {
                return new()
                {
                    Id = Guid.Empty,
                    Name = string.Empty
                };
            }

            return role.MapToDto();
        }
    }
}
