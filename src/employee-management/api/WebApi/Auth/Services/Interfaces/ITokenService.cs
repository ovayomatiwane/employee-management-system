namespace WebApi.Auth.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, string role);
    }
}
