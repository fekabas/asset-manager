using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/file")]
[ApiController]
public class FileController : Controller
{
    public FileController() {
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
        string uploads = Path.Combine("./", "VolumeStorage");
        string filePath = Path.Combine(uploads, file.FileName);
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Download(Guid id){
        // Find metadata in DB
        // Find file in memory
        // Return file
        return Ok();
    }
}