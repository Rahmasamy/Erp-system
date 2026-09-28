using Aspire.Services;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;

namespace Aspire.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly SpacesService _spaces;

    public FilesController(SpacesService spaces)
    {
        _spaces = spaces;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is required.");

        var key = $"uploads/{Guid.NewGuid()}-{file.FileName}";

        await using var stream = file.OpenReadStream();

        await _spaces.UploadAsync(
            stream,
            key,
            file.ContentType
        );

        string downloadUrl = string.Empty;
        try
        {
            downloadUrl = _spaces.GetPresignedUrl(key);
        }
        catch
        {
            downloadUrl = _spaces.GetFileUrl(key);
        }

        return Ok(new
        {
            key,
            fileName = file.FileName,
            fileSize = file.Length,
            contentType = file.ContentType,
            downloadUrl
        });
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery] string key, [FromQuery] string? fileName = null)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest("Key is required.");

        try
        {
            var (stream, contentType, defaultFileName) = await _spaces.DownloadAsync(key);
            var downloadFileName = !string.IsNullOrWhiteSpace(fileName) ? fileName : defaultFileName;
            return File(stream, contentType ?? "application/octet-stream", downloadFileName);
        }
        catch (AmazonS3Exception ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("download-url")]
    public IActionResult GetDownloadUrl([FromQuery] string key, [FromQuery] string? fileName = null)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest("Key is required.");

        try
        {
            var url = _spaces.GetPresignedUrl(key, 60, fileName);
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("preview")]
    public async Task<IActionResult> Preview([FromQuery] string key)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest("Key is required.");

        try
        {
            var (stream, contentType, _) = await _spaces.DownloadAsync(key);
            return File(stream, contentType ?? "application/octet-stream");
        }
        catch (AmazonS3Exception ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("preview-url")]
    public IActionResult GetPreviewUrl([FromQuery] string key)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest("Key is required.");

        try
        {
            var url = _spaces.GetPresignedUrl(key, 60, null);
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] string key)
    {
        if (string.IsNullOrEmpty(key))
            return BadRequest("Key is required.");

        try
        {
            await _spaces.DeleteAsync(key);
            return Ok(new { message = "File deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
