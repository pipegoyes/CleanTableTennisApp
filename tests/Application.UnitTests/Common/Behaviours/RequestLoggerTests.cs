using CleanTableTennisApp.Application.Common.Behaviours;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.TeamMatch.Commands;
using CleanTableTennisApp.Application.TeamMatch.Validators;
using CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;
using CleanTableTennisApp.Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CleanTableTennisApp.Application.UnitTests.Common.Behaviours;

public class CreateTeamMatchCommandValidatorTest
{
    private CreateTeamMatchCommandValidator _testee;

    [SetUp]
    public void Setup()
    {
        _testee = new CreateTeamMatchCommandValidator();
    }

    [Test]
    public void Validate_FullyInitializedRequest_Success()
    {
        var teamMatchCommand = new CreateTeamMatchCommand()
        {
            GuestTeam = new TeamRequest()
            {
                Name = "Guest",
                Players = new List<PlayerRequest>()
                {
                    new PlayerRequest()
                    {
                        DoublePosition = DoublePosition.FirstDouble, FullName = "Felipe Goyes",
                    }
                }
            }
        };

        var result = _testee.Validate(teamMatchCommand);
        result.Errors.Should().BeEmpty();
    }

}

public class RequestLoggerTests
{
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }

    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;
    private Mock<ILogger<CreateTodoItemCommand>> _logger = null!;
}
