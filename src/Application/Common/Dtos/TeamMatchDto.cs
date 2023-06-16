namespace CleanTableTennisApp.Application.Common.Dtos;

public class TeamMatchDto
{
    public string HostTeamName { get; set; }
    public string GuestTeamName { get; set; }
    public DateTime StartedAt { get; set; }
    public int HostVictories { get; set; }
    public int GuestVictories { get; set; }
    public string TeamMatchIdEncoded { get; set; }
}