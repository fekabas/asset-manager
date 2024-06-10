namespace WebAPI.StorageService.AWSS3;
public class FileDTO
{
    public FileDTO(string path)
    {
        FilePath = path;
    }
    public string FilePath { get; set; }
}