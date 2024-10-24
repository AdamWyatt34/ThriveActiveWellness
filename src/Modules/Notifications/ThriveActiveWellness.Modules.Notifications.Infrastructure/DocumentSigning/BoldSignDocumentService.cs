using BoldSign.Api;
using BoldSign.Model;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.DocumentSigning;

public class BoldSignDocumentService(ApiClient apiClient, ITokenService tokenService) : IDocumentService
{
    public async Task SendDocumentForSignatureAsync()
    {
        string accessToken = await tokenService.GetAccessTokenAsync();
        
        apiClient.Configuration.DefaultHeader.Remove("Authorization");
        apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);

        var documentClient = new DocumentClient(apiClient);

        await documentClient.SendDocumentAsync(new SendForSign());
    }
}
