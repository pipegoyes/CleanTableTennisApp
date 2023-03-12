using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.TeamMatch.Commands;
using CleanTableTennisApp.Domain.Enums;
using NSwag.Examples;

namespace CleanTableTennisApp.WebUI.RequestExamples;

public class CreateTeamMatchExample : IExampleProvider<CreateTeamMatchCommand>
{
    public CreateTeamMatchCommand GetExample()
    {
        return new CreateTeamMatchCommand
        {
            HostTeam = new TeamRequest
            {
                Name = "Host",
                Players = new List<PlayerRequest>
                {
                    new()
                    {
                        FullName = "First player",
                        DoublePosition = DoublePosition.FirstDouble
                    },
                    new()
                    {
                        FullName = "Second player",
                        DoublePosition = DoublePosition.FirstDouble
                    },
                    new()
                    {
                        FullName = "Third player",
                        DoublePosition = DoublePosition.SecondDouble
                    },
                    new()
                    {
                        FullName = "Fourth player",
                        DoublePosition = DoublePosition.SecondDouble
                    },
                }
            },
            GuestTeam = new TeamRequest
            {
                Name = "Guest",
                Players = new List<PlayerRequest>
                {
                    new()
                    {
                        FullName = "First player",
                        DoublePosition = DoublePosition.FirstDouble
                    },
                    new()
                    {
                        FullName = "Second player",
                        DoublePosition = DoublePosition.FirstDouble
                    },
                    new()
                    {
                        FullName = "Third player",
                        DoublePosition = DoublePosition.SecondDouble
                    },
                    new()
                    {
                        FullName = "Fourth player",
                        DoublePosition = DoublePosition.SecondDouble
                    },
                }
            }
        };
    }
}
