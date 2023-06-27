using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Interfaces;

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

public interface IScoreDtoConverter
{
    ScoreDto ToDto(IGamePoints gamePoints);
}

public class ScoreDtoConverter : IScoreDtoConverter
{
    private readonly IUrlSafeIntEncoder _encoder;

    public ScoreDtoConverter(IUrlSafeIntEncoder encoder)
    {
        _encoder = encoder;
    }

    public ScoreDto ToDto(IGamePoints gamePoints)
    {
        return new ScoreDto
        {
            GuestPoints = gamePoints.GamePointsGuest,
            HostPoints = gamePoints.GamePointsHost,
            ScoreIdEncoded = _encoder.Encode(gamePoints.Id) 
        };
    }
}