using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Application.Wizard.Validators;
using CleanTableTennisApp.Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
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
        var teamMatchCommand = new CreateTeamMatchCommand
        {
            GuestTeam = new TeamRequest
            {
                Name = "Guest",
                Players = new List<PlayerRequest>
                {
                    new PlayerRequest
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