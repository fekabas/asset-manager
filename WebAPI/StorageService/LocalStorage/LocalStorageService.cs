
namespace WebAPI.StorageService.LocalStorage;
public class LocalStorageService : ILocalStorageService
{
    private string VolumeStoragePath;
    public LocalStorageService()
    {
        // TODO: Make directory configurable
        this.VolumeStoragePath = Path.Combine("./", "VolumeStorage");
    }
    public async Task<byte[]?> Download(FileDTO id)
    {
        // Find file in memory
        if (string.IsNullOrEmpty(id.FilePath) || !File.Exists(id.FilePath))
            return null;

        // Read file content into memory
        byte[] fileBytes = await File.ReadAllBytesAsync(id.FilePath);
        
        return fileBytes;
    }

    public async Task<FileDTO> Upload(IFormFile file)
    {
        // TODO: Parse input
        // TODO: Check if file already exists
        // Save file to memory
        string filePath = Path.Combine(this.VolumeStoragePath, file.FileName);
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return new FileDTO(filePath);
    }
}