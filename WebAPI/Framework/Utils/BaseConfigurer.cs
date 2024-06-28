namespace WebAPI.Framework.Utils;
public class BaseConfigurer
{
    public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env){
        return services;
    }
}