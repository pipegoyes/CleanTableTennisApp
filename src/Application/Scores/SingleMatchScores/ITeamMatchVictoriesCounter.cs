using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Scores.SingleMatchScores;

// think about having this information inside of the entity
public interface ITeamMatchVictoriesCounter
{
    int CountVictories(TeamMatch teamMatch, SetVictory setVictory);
}