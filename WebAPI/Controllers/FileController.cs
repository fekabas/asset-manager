using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess;
using WebAPI.Model.Entities;
using WebAPI.StorageService;
using WebAPI.StorageService.AWSS3;
using WebAPI.StorageService.LocalStorage;

namespace WebAPI.Controllers;

[Route("api/file")]
[ApiController]
//[Authorize]
[AllowAnonymous]
public class FileController : Controller
{
    private readonly IFileMetadataRepository fileMetadataRepository;
    private readonly IAWSS3Storage awsStorage;
    private readonly ILocalStorageService storageService;
    public FileController(IFileMetadataRepository fileMetadataRepository, ILocalStorageService storageService, IAWSS3Storage awsStorage) {
        this.fileMetadataRepository = fileMetadataRepository;
        this.storageService = storageService;
        this.awsStorage = awsStorage;
    }

    /// <summary>
    /// Upload a file
    /// </summary>
    /// <param name="file">Form input of type file</param>
    /// <returns>The UUID of the generated file entry</returns>
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file){
        WebAPI.StorageService.LocalStorage.FileDTO result = await storageService.Upload(file);

        FileMetadata fileMetadata = new FileMetadata()
        {
            Name = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            FileId = result.FilePath
        };

        await fileMetadataRepository.AddAsync(fileMetadata);

        return Ok(new { fileMetadata.Id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Download(Guid id){
        // Find metadata in DB
        FileMetadata? fileMetadata = await fileMetadataRepository.GetAsync(id);
        
        if(fileMetadata is null)
            return NotFound("File does not exist.");

        byte[]? fileBytes = await storageService.Download(new WebAPI.StorageService.LocalStorage.FileDTO(fileMetadata.FileId));
        if(fileBytes is null)
            return NotFound("File not found");
        
        // Return file
        return File(new MemoryStream(fileBytes), fileMetadata.ContentType, fileMetadata.Name);
    }

    [HttpPost("aws")]
    public async Task<IActionResult> AWSUpload(IFormFile file){
        WebAPI.StorageService.AWSS3.FileDTO result = await awsStorage.Upload(file);

        FileMetadata fileMetadata = new FileMetadata()
        {
            Name = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            FileId = result.FilePath
        };

        await fileMetadataRepository.AddAsync(fileMetadata);

        return Ok(new { fileMetadata.Id });
    }

    [HttpGet("aws/{id}")]
    public async Task<IActionResult> AWSDownload(Guid id){
        // Find metadata in DB
        FileMetadata? fileMetadata = await fileMetadataRepository.GetAsync(id);
        
        if(fileMetadata is null)
            return NotFound("File does not exist.");

        byte[]? fileBytes = await awsStorage.Download(new WebAPI.StorageService.AWSS3.FileDTO(fileMetadata.FileId));
        if(fileBytes is null)
            return NotFound("File not found");
        
        // Return file
        return File(new MemoryStream(fileBytes), fileMetadata.ContentType, fileMetadata.Name);
    }
}
