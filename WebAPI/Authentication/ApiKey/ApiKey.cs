namespace WebAPI.Authentication.ApiKey;

/// <summary>
/// Class to model the data that represents an authentication credentials.
/// This includes the real value od the API Key, but also other metadata
/// that gives context to the particular API Key.
/// </summary>
public class ApiKey
{
    public int? ApiKeyId { get; set; }
    public Guid Key { get; set; }
    public string? Name { get; set; }
}