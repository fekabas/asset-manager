using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using WebAPI.Authentication.ApiKey;

/// <summary>
/// Class that implements the Api Key authentication.
/// It leverages AspNetCore.authentication api.
/// We inherit from it's base class and pass the Scheme options
/// we defined for this scheme implementation.
/// </summary>
public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeySchemeOptions>
{
    public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeySchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Request must have the specified header
        if(!Request.Headers.ContainsKey(Options.HeaderName))
            return AuthenticateResult.Fail("API Key header not found.");

        string? headerValue = Request.Headers[Options.HeaderName];
        // TODO: Retrieve Api Key from DB. This line should be the following:
        // ApiKey apiKey = await apiKeyService.GetAsync(headerValue)
        // The implementation of this method should be completly transparent for this class.
        ApiKey apiKey = new ApiKey(){ ApiKeyId = 0, Name = "apikey123", Key = Guid.NewGuid() };

        if(string.IsNullOrEmpty(headerValue) || headerValue != apiKey.Name) // This has to compare with Key, not Name
            return AuthenticateResult.Fail("The API Key is not valid");

        // At this point, the user has provided a valid authentication.
        // Now we have to buil his security context.
        // Meaning the claims that build his identity (one of may me more than one)
        // Then we need to add this identity to the claims principal,
        // which actually represents the user, holding every one of his possible
        // identities.

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, $"{apiKey.ApiKeyId}"),
            new Claim(ClaimTypes.Name, $"{apiKey.Name}")
        };

        var identiy = new ClaimsIdentity(claims, nameof(ApiKeyAuthenticationHandler));

        // This is the ASP.NetCore object that represents the user of the request.
        // ClaimsPrincipal can contain more than one identity.
        var principal = new ClaimsPrincipal(identiy);
        
        AuthenticationTicket ticket = new AuthenticationTicket(principal, null, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}