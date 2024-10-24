using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.DocumentSigning;

public class BoldSignTokenService(HttpClient httpClient, IConfiguration config) : ITokenService
{
    private readonly string _clientId = config["BoldSign:ClientId"]!;
    private readonly string _clientSecret = config["BoldSign:ClientSecret"]!;
    private string? _accessToken;
    private DateTime? _expiresAt;

    // Update these with IOptions

    public async Task<string> GetAccessTokenAsync()
    {
        if (_accessToken != null && _expiresAt != null && _expiresAt > DateTime.UtcNow.AddMinutes(-5))
        {
            return _accessToken;
        }

        var parameters = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
            new("scope", "BoldSign.Documents.All BoldSign.Templates.All")
        };

        using var encodedContent = new FormUrlEncodedContent(parameters);
        using var request = new HttpRequestMessage(HttpMethod.Post, "https://account.boldsign.com/connect/token");
        request.Content = encodedContent;

        string encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);

        HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        
        string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        Dictionary<string, string>? tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
        string accessToken = string.Empty;
        tokenResponse?.TryGetValue("access_token", out accessToken);
        
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new Exception("Failed to get BoldSign access token");
        }
        
        string expiresIn = string.Empty;
        
        _accessToken = accessToken;
        tokenResponse?.TryGetValue("expires_in", out expiresIn);
        
        _expiresAt = DateTime.UtcNow.AddSeconds(Convert.ToInt32(expiresIn, CultureInfo.InvariantCulture));

        return _accessToken;
    }
}

