namespace CleanTableTennisApp.Application.Requests;

public class TeamRequest
{
    public string Name { get; set; } = string.Empty;
    public IList<PlayerRequest> Players { get; set; } = new List<PlayerRequest>();
}