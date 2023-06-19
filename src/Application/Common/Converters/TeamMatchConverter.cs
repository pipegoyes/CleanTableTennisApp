using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Domain.Entities;

namespace CleanTableTennisApp.Application.Common.Converters;

// todo think about a generic approach
public class TeamMatchConverter : ITeamMatchConverter
{
    private readonly IUrlSafeIntEncoder _encoder;

    public TeamMatchConverter(IUrlSafeIntEncoder encoder)
    {
        _encoder = encoder;
    }

    public TeamMatchDto ToDto(TeamMatch match)
    {
        return new TeamMatchDto
        {
            GuestTeamName = match.GuestTeam.Name,
            HostTeamName = match.HostTeam.Name,
            GuestVictories = 0,
            HostVictories = 0,
            StartedAt = match.Created,
            TeamMatchIdEncoded = _encoder.Encode(match.Id)
        };
    }
}