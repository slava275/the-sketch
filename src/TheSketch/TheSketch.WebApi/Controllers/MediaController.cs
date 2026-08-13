using Microsoft.AspNetCore.Mvc;
using TheSketch.Application.Interfaces.Services.External;

namespace TheSketch.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MediaController : ControllerBase
{
    private readonly IImageUploadService _imageUploadService;

    public MediaController(IImageUploadService imageUploadService)
    {
        _imageUploadService = imageUploadService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не обрано");

        await using var stream = file.OpenReadStream();

        var imageUrl = await _imageUploadService.UploadImageAsync(stream, file.FileName);

        return Ok(new { url = imageUrl });
    }
}
