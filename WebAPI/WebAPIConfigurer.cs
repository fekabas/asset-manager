using WebAPI.Authentication;
using WebAPI.DataAccess;
using WebAPI.StorageService;

namespace WebAPI;

internal static class WebAPIConfigurer
{
    private static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // configure authentication to be used
        //builder.Services.AddAuthentication(AuthentucationConfigurer.Configure);
        services.ConfigureAuthentication(configuration, env);

        // Add and configure DataAccess layer
        services.ConfigureDataAccess(configuration, env);

        // Add and configure StorageService
        services.ConfigureStorageService(configuration, env);

        return services;
    }
    public static IServiceCollection ConfigureWebAPI(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    => Configure(services, configuration, env);
}