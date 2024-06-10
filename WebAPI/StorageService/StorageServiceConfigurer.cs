using WebAPI.StorageService.LocalStorage;

namespace WebAPI.StorageService;

internal static class StorageServiceConfigurer
{
    public static IServiceCollection ConfigureStorageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();

        return services;
    }
}