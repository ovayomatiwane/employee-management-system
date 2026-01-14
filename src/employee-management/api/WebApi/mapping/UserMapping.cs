using WebApi.Dtos;
using WebApi.Models.Entities;

namespace WebApi.mapping
{
    public static class UserMapping
    {
        public static UserDto MapToDto(this User user)
        {
            return new()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };
        }

        public static IEnumerable<UserDto> MapToDtos(this IEnumerable<User> users)
        {
            return users.Select(x => x.MapToDto());
        }
    }
}
