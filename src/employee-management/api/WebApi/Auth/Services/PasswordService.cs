using Microsoft.AspNetCore.Identity;
using WebApi.Auth.Services.Interfaces;
using WebApi.Models.Entities;

namespace WebApi.Auth.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();
        private readonly User user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Surname = "Surname",
            Email = "Email",
            PasswordHash = "PasswordHash"
        };

        public string HashPassword(string password)
        {
            // Temp user that is used for the hashing of the password
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(string hash, string password)
        {
            var result = _hasher.VerifyHashedPassword(user, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
