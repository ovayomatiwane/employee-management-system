using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ImagesService(
        IBlobStorageService blobStorageService,
        EmployeeManagementDataContext databaseContext) : IImagesService
    {
        public async Task<string> UploadConsultantImageAsync(IFormFile file, Guid userId, CancellationToken cancellationToken = default)
        {
            var consultant = await databaseContext.Consultants.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
            if (consultant is null)
            {
                return string.Empty;
            }

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType))
                return "Invalid image type";

            if (file.Length > 5_000_000)
                return "File too large";

            var imageUrl = await blobStorageService.UploadAsync(file);

            consultant.ImageFileName = file.FileName;
            consultant.ImageUrl = imageUrl;

            databaseContext.Consultants.Update(consultant);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return imageUrl;
        }
    }
}
