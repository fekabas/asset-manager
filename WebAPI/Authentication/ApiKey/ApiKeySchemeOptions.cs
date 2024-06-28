using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;

namespace WebAPI.Authentication.ApiKey;

/// <summary>
/// Options that define the API Key authentication implementation.
/// This leverages the AspNetCore.Authentication api.
/// Here we define what parameters define this implementation.
/// For example, we will store the API key in a header.
/// So we need to specify which header.
/// We also have to define a name for the Scheme. This is needed by
/// AspNetcore.Authentication's middleware that handles the authentication
/// strategies used and their excecution at runtime
/// </summary>
public class ApiKeySchemeOptions : AuthenticationSchemeOptions
{
    public const string Name = "ApiKey";
    public const string Scheme = "ApiKeyScheme";
    /// <summary>
    /// Nombre del Header donde se buscará la API Key
    /// Default: Authorization
    /// </summary>
    /// <value></value>
    public string HeaderName { get; set; } = HeaderNames.Authorization;
} 