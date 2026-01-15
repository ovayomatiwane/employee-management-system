using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.QueryData;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IImagesService imagesService) : ControllerBase
    {
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAsync([FromForm] ImageUploadRequestDto request)
        {
            if (request is null || request.File == null || request.File.Length == 0)
                return BadRequest("No file uploaded");

            var imageUrl = await imagesService.UploadConsultantImageAsync(request.File, request.UserId);

            return Ok(imageUrl);
        }
    }
}
