namespace CleanTableTennisApp.Application.Requests;

public class CreateTeamPlayersDto
{
    public string Name { get; set; } = string.Empty;
    public IList<CreatePlayerDto> Players { get; set; } = new List<CreatePlayerDto>();
}
