namespace WebApi.Dtos.QueryData
{
    public class ImageUploadRequestDto
    {
        public Guid UserId { get; set; }

        public IFormFile File { get; set; } = null!;
    }
}
