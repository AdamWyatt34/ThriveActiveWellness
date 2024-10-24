namespace ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

public interface IDocumentService
{
    //  Provide a way to pass in the information needed to generate the document
    Task SendDocumentForSignatureAsync();
}
