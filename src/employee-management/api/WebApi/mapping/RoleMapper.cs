using WebApi.Dtos;
using WebApi.Models.Entities;

namespace WebApi.mapping
{
    public static class RoleMapper
    {
        public static Role MapToEntity(this RoleDto roleDto)
        {
            return new()
            {
                Id = roleDto.Id,

                Name = roleDto.Name,

                HourlyRate = roleDto.HourlyRate,

                CreatedDate = roleDto.CreatedDate,

                RemovedDate = roleDto.RemovedDate,
            };
        }

        public static IEnumerable<Role> MapToEntities(this IEnumerable<RoleDto> roleDtos)
        {
            return roleDtos.Select(x => x.MapToEntity());
        }

        public static RoleDto MapToDto(this Role role)
        {
            return new()
            {
                Id = role.Id,

                Name = role.Name,

                HourlyRate = role.HourlyRate,

                CreatedDate = role.CreatedDate,

                RemovedDate = role.RemovedDate,
            };
        }

        public static IEnumerable<RoleDto> MapToDtos(this IEnumerable<Role> roles)
        {
            return roles.Select(x => x.MapToDto());
        }
    }
}
