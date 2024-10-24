namespace ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQResponsesByUser;

public sealed record ParqResponseRecord(int Id, int QuestionId, string Question, string Answer);

