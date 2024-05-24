using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/file")]
[ApiController]
public class FileController : Controller
{
    public FileController(){

    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="file">Form input of type file</param>
    /// <returns>The UUID of the generated file entry</returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] IFormFile file){
        // Parse input
        // Retrieve metada
        // Save file to memory
        // Insert metadata into DB with the fiel path data
        // Return UUID of metadata
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