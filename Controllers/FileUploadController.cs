using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Byway.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class FileUploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<FileUploadController> _logger;

    public FileUploadController(IWebHostEnvironment environment, ILogger<FileUploadController> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            // Validate file type
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
            {
                return BadRequest(new { message = "Invalid file type. Only JPEG, PNG, GIF, and WebP images are allowed." });
            }

            // Validate file size (max 5MB)
            if (file.Length > 5 * 1024 * 1024)
            {
                return BadRequest(new { message = "File size too large. Maximum size is 5MB." });
            }

            // Create uploads directory if it doesn't exist
            var uploadsPath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "uploads", "images");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // Generate unique filename
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the relative path that can be used in the application
            var relativePath = $"/uploads/images/{fileName}";

            return Ok(new { 
                message = "File uploaded successfully", 
                filePath = relativePath,
                fileName = fileName,
                originalName = file.FileName,
                size = file.Length
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file");
            return StatusCode(500, new { message = "An error occurred while uploading the file" });
        }
    }

    [HttpDelete("image")]
    public IActionResult DeleteImage([FromQuery] string filePath)
    {
        try
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest(new { message = "File path is required" });
            }

            // Remove leading slash if present
            var cleanPath = filePath.TrimStart('/');
            var fullPath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, cleanPath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return Ok(new { message = "File deleted successfully" });
            }

            return NotFound(new { message = "File not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file: {FilePath}", filePath);
            return StatusCode(500, new { message = "An error occurred while deleting the file" });
        }
    }
}