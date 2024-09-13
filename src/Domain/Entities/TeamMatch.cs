using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class TeamMatch : AuditableEntity
{
    public const int MinimumSetsWon = 3;
    static readonly char[] Padding = { '=' };

    // TODO EF needs a parameterless constructor 
    public TeamMatch()
    {
        HostTeam = new Team(string.Empty);
        GuestTeam = new Team(string.Empty);
        SingleMatches = new List<SingleMatch>();
        DoubleMatches = new List<DoubleMatch>();
    }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
        SingleMatches = new List<SingleMatch>();
        DoubleMatches = new List<DoubleMatch>();
    }

    public int Id { get; set; }
    public Team HostTeam { get; set; }
    public Team GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }
    public ICollection<SingleMatch> SingleMatches { get; set; }
    public ICollection<DoubleMatch> DoubleMatches { get; set; }
    public string Slug
    {
        get
        {
            byte[] numBytes = BitConverter.GetBytes(Id);
            return Convert.ToBase64String(numBytes)
                .TrimEnd(Padding).Replace('+', '-').Replace('/', '_');
        }
    }

    public int CountVictories(SetVictory setVictory)
    {
        return CalculateMatchVictories().Count(s => s == setVictory);
    }

    private IReadOnlyCollection<SetVictory> CalculateMatchVictories()
    {
        var result = new List<SetVictory>();

        // todo Can I use Iscoreable<> to avoid duplication?
        //var allMatches = teamMatch.SingleMatches.Cast<IScorable<IGamePoints>>().Concat(teamMatch.DoubleMatches.Cast<IScorable<IGamePoints>>());

        foreach (var singleMatches in this.SingleMatches)
        {
            result.Add(CalculateSetVictory(singleMatches.Scores.ToArray()));
        }
        foreach (var doubleMatch in this.DoubleMatches)
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

    public static int Decode(string encodedString)
    {
        string incoming = encodedString
            .Replace('_', '/').Replace('-', '+');
        switch (encodedString.Length % 4)
        {
            case 2: incoming += "=="; break;
            case 3: incoming += "="; break;
        }
        byte[] bytes = Convert.FromBase64String(incoming);
        return BitConverter.ToInt32(bytes);
    }
}
