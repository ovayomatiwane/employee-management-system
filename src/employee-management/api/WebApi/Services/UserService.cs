using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WebApi.Auth.Services.Interfaces;
using WebApi.CustomExceptions;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class UserService(EmployeeManagementDataContext databaseContext, IPasswordService passwordService) : IUserService
    {
        public async Task<bool> IsUserRegisteredAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(x => x.Email == request.Username, cancellationToken);

            if (user is null)
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto> RegisterUserAsync(NewUserDto user, CancellationToken cancellationToken = default)
        {
            ValidateNewUser(user);

            Guid id = Guid.NewGuid();

            var password = passwordService.HashPassword(user.Password);

            User userEntity = new()
            {
                Id = id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                PasswordHash = password,
            };

            databaseContext.Users.Add(userEntity);

            await databaseContext.SaveChangesAsync(cancellationToken);

            return userEntity.MapToDto();
        }

        public async Task<bool> VerifyLoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(x => x.Email == request.Username, cancellationToken);

            if (user is null)
            {
                return false;
            }

            var isPasswordCorrect = passwordService.VerifyPassword(user.PasswordHash, request.Password);

            return isPasswordCorrect;
        }

        private void ValidateNewUser(NewUserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException(nameof(user.Email));
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentException(nameof(user.Name));
            }

            if (string.IsNullOrWhiteSpace(user.Surname))
            {
                throw new ArgumentException(nameof(user.Surname));
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException(nameof(user.Email));
            }
        }
    }
}
