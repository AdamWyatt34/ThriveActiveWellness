using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Notifications.Application.PARQ.GenerateParqPdf;

public sealed record GenerateParqPdfCommand : ICommand<byte[]>
{
    public GenerateParqPdfCommand(Guid userId, List<ParQItemModel> parQItems)
    {
        UserId = userId;
        ParQItems = parQItems;
    }

    public Guid UserId { get; init; }

    public List<ParQItemModel> ParQItems { get; init; }
}

public sealed class ParQItemModel
{
    public int Id { get; init; }
    public int QuestionId { get; init; }
    public string Question { get; init; }
    public string Answer { get; init; }
}

