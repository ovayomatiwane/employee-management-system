namespace WebApi.Services.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
