using WebApi.Dtos;
using WebApi.Models.Entities;

namespace WebApi.mapping
{
    public static class RoleRateMapper
    {
        public static RoleRate MapToEntity(this RoleRateDto roleRateDto)
        {
            return new()
            {
                Id = roleRateDto.Id,
                HourlyRate = roleRateDto.HourlyRate,
                RoleId = roleRateDto.RoleId,
                AssignedDate = roleRateDto.AssignedDate,
                Role = roleRateDto.Role.MapToEntity(),
                StartDate = roleRateDto.StartDate,
                EndDate = roleRateDto.EndDate,
            };
        }

        public static IEnumerable<RoleRate> MapToEntities(this IEnumerable<RoleRateDto> roleDtos)
        {
            return roleDtos.Select(x => x.MapToEntity());
        }

        public static RoleRateDto MapToDto(this RoleRate roleRate)
        {
            return new()
            {
                Id = roleRate.Id,

                HourlyRate = roleRate.HourlyRate,

                RoleId = roleRate.RoleId,

                AssignedDate = roleRate.AssignedDate,

                Role = roleRate.Role.MapToDto(),

                StartDate = roleRate.StartDate,

                EndDate = roleRate.EndDate,
            };
        }

        public static IEnumerable<RoleRateDto> MapToDtos(this IEnumerable<RoleRate> roles)
        {
            return roles.Select(x => x.MapToDto());
        }
    }
}
