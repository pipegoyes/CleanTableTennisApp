using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Scores.SingleMatchScores;

public class TeamMatchVictoriesCounter : ITeamMatchVictoriesCounter
{
    public const int MinimumSetsWon = 3;

    public int CountVictories(TeamMatch teamMatch, SetVictory setVictory)
    {
        return CalculateMatchVictories(teamMatch).Count(s => s == setVictory);
    }

    private static IReadOnlyCollection<SetVictory> CalculateMatchVictories(TeamMatch teamMatch)
    {
        var result = new List<SetVictory>();

        // todo Can I use Iscoreable<> to avoid duplication?
        //var allMatches = teamMatch.SingleMatches.Cast<IScorable<IGamePoints>>().Concat(teamMatch.DoubleMatches.Cast<IScorable<IGamePoints>>());

        foreach (var singleMatches in teamMatch.SingleMatches)
        {
            result.Add(CalculateSetVictory(singleMatches.Scores.ToArray()));
        }
        foreach (var doubleMatch in teamMatch.DoubleMatches)
        {
            result.Add(CalculateSetVictory(doubleMatch.Scores.ToArray()));
        }

        return result;
    }


    private static SetVictory CalculateSetVictory(IReadOnlyCollection<IGamePoints> scores)
    {
        var hostWonSets = scores.Count(s => s.GamePointsHost > s.GamePointsGuest);
        var guestWonSets = scores.Count(s => s.GamePointsGuest > s.GamePointsHost);

        if (hostWonSets > guestWonSets && hostWonSets >= MinimumSetsWon)
        {
            return SetVictory.HostWon;
        }
        if (guestWonSets > hostWonSets && guestWonSets >= MinimumSetsWon)
        {
            return SetVictory.GuestWon;
        }

        return SetVictory.StillUnknown;
    }


}
