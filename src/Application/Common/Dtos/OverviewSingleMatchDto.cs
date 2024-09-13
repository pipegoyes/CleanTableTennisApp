using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Common.Dtos;

/// <summary>
/// It is another simplified version of a single match.
/// </summary>
public class OverviewSingleMatchDto
{
    public required string MatchIdEncoded { get; set; }
    public required string HostPlayerName { get; set; }
    public required string GuestPlayerName { get; set; }
    public int HostPoints { get; set; }
    public int GuestPoints { get; set; }
    public PlayingOrder PlayingOrder { get; set; }
    public required IEnumerable<ScoreDto> ScoresDtos { get; set; }
}
