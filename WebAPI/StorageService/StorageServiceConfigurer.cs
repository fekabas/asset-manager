using WebAPI.StorageService.AWSS3;
using WebAPI.StorageService.LocalStorage;
using Amazon.S3;

namespace WebAPI.StorageService;

internal static class StorageServiceConfigurer
{
    public static IServiceCollection ConfigureStorageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IAWSS3Storage,AWSS3Storage>();

        return services;
    }
}