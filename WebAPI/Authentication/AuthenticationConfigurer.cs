using Microsoft.AspNetCore.Authentication;
using WebAPI.Authentication.ApiKey;

namespace WebAPI.Authentication;

/// <summary>
/// Encapsule Authentication configuration.
/// This configuration method should be consumed by "AddAuthentication" extension in Startup.cs/Program.cs
/// </summary>
public static class AuthentucationConfigurer
{
    public static void Configure(AuthenticationOptions options)
    {
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
    }
}