using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Scores;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Common.Converters;

// todo think about a generic approach
public class TeamMatchConverter : ITeamMatchConverter
{
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly ITeamMatchVictoriesCounter _teamMatchVictoriesCounter;

    public TeamMatchConverter(IUrlSafeIntEncoder encoder, ITeamMatchVictoriesCounter teamMatchVictoriesCounter)
    {
        _encoder = encoder;
        _teamMatchVictoriesCounter = teamMatchVictoriesCounter;
    }

    public TeamMatchDto ToDto(TeamMatch match)
    {
        return new TeamMatchDto
        {
            GuestTeamName = match.GuestTeam.Name,
            HostTeamName = match.HostTeam.Name,
            GuestVictories = _teamMatchVictoriesCounter.CountVictories(match, SetVictory.GuestWon),
            HostVictories = _teamMatchVictoriesCounter.CountVictories(match, SetVictory.HostWon),
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