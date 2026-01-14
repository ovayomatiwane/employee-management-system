using WebApi.Dtos;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(NewUserDto user, CancellationToken cancellationToken = default);

        Task<bool> IsUserRegisteredAsync(LoginRequestDto user, CancellationToken cancellationToken = default);

        Task<bool> VerifyLoginAsync(LoginRequestDto user, CancellationToken cancellationToken = default);
    }
}
