namespace CleanTableTennisApp.Application.Common.Dtos;

public class TeamMatchDto
{
    public string HostTeamName { get; set; } = string.Empty;
    public string GuestTeamName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public int HostVictories { get; set; }
    public int GuestVictories { get; set; }
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}
