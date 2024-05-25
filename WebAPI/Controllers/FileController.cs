using System.Data.SqlTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebAPI.Controllers;

[Route("api/file")]
[ApiController]
public class FileController : Controller
{
    private string VolumeStoragePath;
    public FileController() {
        this.VolumeStoragePath = Path.Combine("./", "VolumeStorage");
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="file">Form input of type file</param>
    /// <returns>The UUID of the generated file entry</returns>
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file){
        // TODO: Parse input
    
        // TODO: Check if file already exists
        // Save file to memory
        // TODO: Make directory configurable
        string filePath = Path.Combine(this.VolumeStoragePath, file.FileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        // Retrieve metada
        var type = file.ContentType;
        var length = file.Length;
        var name = file.FileName;
        var createdDate = DateTime.UtcNow;
        var path = filePath;

        // TODO: Insert metadata into DB with the field path data
        // TODO: Return UUID of metadata

        return Ok();
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> Download(string fileName){
        // TODO: Find metadata in DB


        // Find file in memory
        string filePath = Path.Combine(this.VolumeStoragePath, fileName);
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            return NotFound("File not found.");
        }

        // Read file content into memory
        byte[] fileBytes;
        try
        {
            fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        // Determine the content type based on the file extension
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        
        // Return file
        return File(new MemoryStream(fileBytes), contentType, fileName);
    }
}