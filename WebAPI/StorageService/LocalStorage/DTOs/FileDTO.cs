namespace WebAPI.StorageService.LocalStorage;

public class FileDTO
{
    internal FileDTO(string path)
    {
        FilePath = path;
    }
    public string FilePath { get; set; }
}