using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.Application.TeamMatches.Validators;
using CleanTableTennisApp.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace CleanTableTennisApp.Application.UnitTests.Common.Behaviours;

public class CreateTeamMatchCommandValidatorTest
{
    private CreateTeamMatchCommandValidator _testee = new();

    [SetUp]
    public void Setup()
    {
        _testee = new CreateTeamMatchCommandValidator();
    }

    [Test]
    public void Validate_FullyInitializedRequest_Success()
    {
        var teamMatchCommand = new CreateTeamMatchCommand
        {
            GuestTeam = new TeamRequest
            {
                Name = "Guest",
                Players = new List<PlayerRequest>
                {
                    CreatePlayer("Felipe Goyes", DoublePosition.FirstDouble),
                    CreatePlayer("Andres Coral", DoublePosition.FirstDouble),
                    CreatePlayer("Bran Muster", DoublePosition.SecondDouble),
                    CreatePlayer("Jan Peter", DoublePosition.SecondDouble),
                }
            },
            HostTeam = new TeamRequest()
            {
                Name = "Host",
                Players = new List<PlayerRequest>()
                {
                    CreatePlayer("Player1 LastName1", DoublePosition.FirstDouble),
                    CreatePlayer("Player2 LastName2", DoublePosition.FirstDouble),
                    CreatePlayer("Player3 LastName3", DoublePosition.SecondDouble),
                    CreatePlayer("Player4 LastName4", DoublePosition.SecondDouble),
                }
            }
        };

        var result = _testee.Validate(teamMatchCommand);
        result.Errors.Should().BeEmpty();
    }

    private static PlayerRequest CreatePlayer(string name, DoublePosition doublePosition) =>
        new()
        {
            DoublePosition = doublePosition,
            FullName = name,
        };
}
