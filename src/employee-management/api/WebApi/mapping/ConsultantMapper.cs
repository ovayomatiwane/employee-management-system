using WebApi.Dtos;
using WebApi.Models.Entities;

namespace WebApi.mapping
{
    public static class ConsultantMapper
    {
        public static Consultant MapToEntity(this ConsultantDto consultantDto)
        {
            return new()
            {
                Id = consultantDto.Id,
                FirstName = consultantDto.FirstName,
                LastName = consultantDto.LastName,
                EmailAddress = consultantDto.EmailAddress,
            };
        }

        public static IEnumerable<Consultant> MapToEntities(this IEnumerable<ConsultantDto> consultantDtos)
        {
            return consultantDtos.Select(x => x.MapToEntity());
        }

        public static ConsultantDto MapToDto(this Consultant consultant)
        {
            return new()
            {
                Id = consultant.Id,
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
                EmailAddress = consultant.EmailAddress,
            };
        }

        public static IEnumerable<ConsultantDto> MapToDtos(this IEnumerable<Consultant> consultants)
        {
            return consultants.Select(x => x.MapToDto());
        }
    }
}
