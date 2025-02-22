using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQQuestions;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.PARQ;

public class GetParQQuestionsTests : BaseIntegrationTest
{
    public GetParQQuestionsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnQuestions_WhenParentQuestionIdIsProvided()
    {
        // Arrange
        const int parentQuestionId = 4;

        // Act
        Result<IEnumerable<ParqQuestionResponse>> result = await Sender.Send(new GetParQQuestionsQuery(parentQuestionId));

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeEmpty();
    }
    
    [Fact]
    public async Task Should_ReturnEmptyList_WhenParentQuestionIdIsNotProvided()
    {
        // Arrange
        const int parentQuestionId = 0;

        // Act
        Result<IEnumerable<ParqQuestionResponse>> result = await Sender.Send(new GetParQQuestionsQuery(parentQuestionId));

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }
}
