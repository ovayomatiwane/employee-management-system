namespace WebApi.Services.Interfaces
{
    public interface IImagesService
    {
        Task<string> UploadConsultantImageAsync(IFormFile file, Guid userId, CancellationToken cancellationToken = default);
    }
}
