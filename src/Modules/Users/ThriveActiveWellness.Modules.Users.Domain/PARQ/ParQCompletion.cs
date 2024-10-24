using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.PARQ;

public class ParQCompletion : Entity
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CompletionDate { get; private set; }
    public string PdfUrl { get; private set; }
    
    private ParQCompletion() { }

    public static ParQCompletion Create(Guid userId)
    {
        var completion = new ParQCompletion
        {
            UserId = userId,
            CompletionDate = DateTime.UtcNow
        };
        
        completion.Raise(new UserParQCompletedDomainEvent(userId));
        
        return completion;
    }

    public void SetPdfUrl(string pdfUrl)
    {
        PdfUrl = pdfUrl;
    }

    public void SetPdfUrl(Uri pdfUrl)
    {
        PdfUrl = pdfUrl.ToString();
    }
}
