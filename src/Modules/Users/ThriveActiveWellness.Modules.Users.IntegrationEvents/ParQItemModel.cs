namespace ThriveActiveWellness.Modules.Users.IntegrationEvents;

public sealed class ParQItemModel
{
    public int Id { get; init; }
    public int QuestionId { get; init; }
    public string Question { get; init; }
    public string Answer { get; init; }
}
