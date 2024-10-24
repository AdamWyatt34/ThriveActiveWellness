using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.PARQ;

public class ParqQuestion : Entity
{
    public int Id { get; private set; }
    public string Question { get; private set; }
    public int? ParentQuestionId { get; private set; }
    // This is the condition type that will be used to determine if the question should be displayed
    public ConditionType? ConditionType { get; private set; }
    public ParqQuestion ParentQuestion { get; private set; }
    public ICollection<ParqQuestion> ConditionalQuestions { get; private set; }
    
    private ParqQuestion() { }

    public static ParqQuestion Create(int questionId, string questionText, int? parentQuestionId = null, ConditionType? condition = null)
    {
        return new ParqQuestion
        {
            Id = questionId,
            Question = questionText,
            ParentQuestionId = parentQuestionId,
            ConditionType = condition
        };
    }
}
