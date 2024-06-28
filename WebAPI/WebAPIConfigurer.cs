using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using WebAPI.Authentication;
using WebAPI.Authentication.ApiKey;
using WebAPI.DataAccess;
using WebAPI.StorageService;

namespace WebAPI;

internal static class WebAPIConfigurer
{
    private static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Manager API", Version = "v1" }); });

        // configure authentication to be used
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