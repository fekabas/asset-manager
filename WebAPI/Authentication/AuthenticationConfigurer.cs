using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using WebAPI.Authentication.ApiKey;

namespace WebAPI.Authentication;

/// <summary>
/// Encapsule Authentication configuration.
/// This configuration method should be consumed by "AddAuthentication" extension in Startup.cs/Program.cs
/// </summary>
public static class AuthentucationConfigurer
{
    public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env, bool addSwagger = true)
    {
        services.AddAuthentication(options => {
            // Add Every possible authentication scheme that this application may use
            // ...
            // options.AddScheme<CookieAuthenticationHandler>(CookieAuthenticationDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme);
            // options.AddScheme<JwtAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme);
            // If using the default Cookie authentication, you just need to use the AddCookie() extension method
            // If using the default JWT authentication, you just need to use the AddJwtBearer() extension method from the Microsoft.AspNetCore.Authentication.JwtBearer package
            // ...
            options.AddScheme<ApiKeyAuthenticationHandler>(ApiKeySchemeOptions.Scheme, ApiKeySchemeOptions.Scheme);

            // Define the default schema to use
            options.DefaultAuthenticateScheme = ApiKeySchemeOptions.Scheme;
        });

        if(addSwagger)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition(ApiKeySchemeOptions.Name, new OpenApiSecurityScheme
                {
                    Description = "ApiKey must appear in header",
                    Type = SecuritySchemeType.ApiKey,
                    Name = HeaderNames.Authorization,
                    In = ParameterLocation.Header,
                    Scheme = ApiKeySchemeOptions.Scheme
                });

                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = ApiKeySchemeOptions.Name
                    },
                    In = ParameterLocation.Header
                };
                
                var requirement = new OpenApiSecurityRequirement
                                {
                                    { key, new List<string>() }
                                };

                c.AddSecurityRequirement(requirement);
            });
        }
        
        return services;
    }

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    => Configure(services, configuration, env);
}