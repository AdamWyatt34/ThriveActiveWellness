using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.PARQ;

public class ParqResponse : Entity
{
    public int Id { get; private set; }
    public Guid UserId { get; private set; }
    public int QuestionId { get; private set; }
    public string Response { get; private set; }
    public DateTime ResponseDate { get; private set; }
    public ParqQuestion Question { get; private set; }
    
    private ParqResponse() { }

    public static ParqResponse Create(Guid userId, int questionId, string answer)
    {
        return new ParqResponse
        {
            UserId = userId,
            QuestionId = questionId,
            Response = answer,
            ResponseDate = DateTime.UtcNow
        };
    }
}
