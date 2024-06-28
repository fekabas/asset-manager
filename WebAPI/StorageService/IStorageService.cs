namespace WebAPI.StorageService;
public interface IStorageService<T> where T : class
{
    /// <summary>
    /// Upload file to storage. Returns an object that represents the file
    /// </summary>
    Task<T> Upload(IFormFile file);

    Task<byte[]?> Download (T id);
}