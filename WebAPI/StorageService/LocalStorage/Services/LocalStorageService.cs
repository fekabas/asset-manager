
namespace WebAPI.StorageService.LocalStorage;
public class LocalStorageService : ILocalStorageService
{
    private readonly ILocalStorageConfiguration localStorageConfiguration;
    public LocalStorageService(ILocalStorageConfiguration localStorageConfiguration)
    {
        this.localStorageConfiguration = localStorageConfiguration;
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
        string filePath = Path.Combine(localStorageConfiguration.VolumeStoragePath, file.FileName);

        // Check if file already exists
        // TODO: Check also a Hash of the file
        if(!File.Exists(filePath))
        {
            string directory = Path.GetDirectoryName(filePath);
        
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            // Save file to memory
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        return new FileDTO(filePath);
    }

    public async Task Delete(FileDTO id)
    {
        // Find file in memory
        if (!string.IsNullOrEmpty(id.FilePath) && File.Exists(id.FilePath))
            File.Delete(id.FilePath);
    }
}