namespace WebAPI.StorageService.LocalStorage;
public interface ILocalStorageConfiguration : IConfiguration
{
    string VolumeStoragePath { get; set; }
}