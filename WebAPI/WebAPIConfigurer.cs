using WebAPI.Authentication;
using WebAPI.DataAccess;
using WebAPI.StorageService;

namespace WebAPI;

internal static class WebAPIConfigurer
{
    public static IServiceCollection ConfigureWebAPI(this IServiceCollection services, IConfiguration configuration)
    {
        // configure authentication to be used
        //builder.Services.AddAuthentication(AuthentucationConfigurer.Configure);
        services.ConfigureAuthentication(configuration);

        // Add and configure DataAccess layer
        services.ConfigureDataAccess(configuration);

        // Add and configure StorageService
        services.ConfigureStorageService(configuration);

        return services;
    }   
}