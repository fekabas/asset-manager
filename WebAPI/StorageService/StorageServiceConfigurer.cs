using WebAPI.StorageService.LocalStorage;

namespace WebAPI.StorageService;

internal static class StorageServiceConfigurer
{
    private static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddSingleton<ILocalStorageConfiguration, LocalStorageConfiguration>(provider => new LocalStorageConfiguration(services.BuildServiceProvider()));
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        return services;
    }
    public static IServiceCollection ConfigureStorageService(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    => Configure(services, configuration, env);
}