using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Common.Dtos;

public class TeamMatchResponse
{
    public string HostTeamName { get; set; } = string.Empty;
    public string GuestTeamName { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public int HostVictories { get; set; }
    public int GuestVictories { get; set; }
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}
public static class TeamMatchReponseExtension
{
    public static TeamMatchResponse ToResponse(this TeamMatch teamMatch)
    {
        return new TeamMatchResponse
        {
            GuestTeamName = teamMatch.GuestTeam.Name,
            HostTeamName = teamMatch.HostTeam.Name,
            GuestVictories = teamMatch.CountVictories(SetVictory.GuestWon),
            HostVictories = teamMatch.CountVictories(SetVictory.HostWon),
            StartedAt = teamMatch.Created,
            TeamMatchIdEncoded = teamMatch.Slug
        };
    }
}
