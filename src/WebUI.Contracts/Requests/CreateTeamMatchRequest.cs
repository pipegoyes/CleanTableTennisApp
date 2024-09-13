

using CleanTableTennisApp.Domain.Enums;

namespace WebUI.Contracts.Requests
{
    public class CreateTeamMatchRequest
    {
        public CreateTeamPlayersRequest HostTeam { get; set; } = new();
        public CreateTeamPlayersRequest GuestTeam { get; set; } = new();
    }

    public class CreateTeamPlayersRequest
    {
        public string Name { get; set; } = string.Empty;
        public IList<CreatePlayerRequest> Players { get; set; } = [];
    }

    public class CreatePlayerRequest
    {
        public string FullName { get; set; } = string.Empty;
        public DoublePosition DoublePosition { get; set; }
    }

}
