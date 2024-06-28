namespace WebAPI.StorageService.LocalStorage;
public interface ILocalStorageService : IStorageService<FileDTO>
{
    Task Delete(FileDTO id);
}