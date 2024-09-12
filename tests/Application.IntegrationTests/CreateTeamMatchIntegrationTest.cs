using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CleanTableTennisApp.Application.IntegrationTests
{
    [Collection(nameof(IntegrationTestLifeTime))]
    public class CreateTeamMatchIntegrationTest
    {
        [Fact]
        public async Task CreateTeamMatch_ShouldSucceed()
        {
            var scope = new IntegrationTestServiceScope();
            
            var request = new CreateTeamMatchCommand
            {
                GuestTeam = new TeamRequest
                {
                    Name = "Guest Team",
                    Players = new List<PlayerRequest>
                    {
                        new() { DoublePosition = DoublePosition.FirstDouble, FullName = "Felipe Goyes" },
                        new() { DoublePosition = DoublePosition.FirstDouble, FullName = "Andres Coral" },
                        new() { DoublePosition = DoublePosition.SecondDouble, FullName = "Julanito Detal" },
                        new() { DoublePosition = DoublePosition.SecondDouble, FullName = "Sutanito Muester" },
                    }
                },
                HostTeam = new TeamRequest
                {
                    Name = "Host Team",
                    Players = new List<PlayerRequest>
                    {
                        new() { DoublePosition = DoublePosition.FirstDouble, FullName = "Felipe Goyes2" },
                        new() { DoublePosition = DoublePosition.FirstDouble, FullName = "Andres Coral2" },
                        new() { DoublePosition = DoublePosition.SecondDouble, FullName = "Julanito Detal2" },
                        new() { DoublePosition = DoublePosition.SecondDouble, FullName = "Sutanito Muester2" },
                    }
                }
            };
            var result = await scope.SendAsync(request);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
