using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewSingleMatchDto
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string HostPlayerName { get; set; } = string.Empty;
    public string GuestPlayerName { get; set; } = string.Empty;
    public int HostPoints { get; set; } = 0;
    public int GuestPoints { get; set; } = 0;
    public PlayingOrder PlayingOrder { get; set; }
    public IEnumerable<ScoreDto> ScoresDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class OverviewDoubleMatchDto
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string HostLeftPlayerName { get; set; } = string.Empty;
    public string HostRightPlayerName { get; set; } = string.Empty;
    public string GuestLeftPlayerName { get; set; } = string.Empty;
    public string GuestRightPlayerName { get; set; } = string.Empty;
    public int HostPoints { get; set; } = 0;
    public int GuestPoints { get; set; } = 0;
    public PlayingOrder PlayingOrder { get; set; }
    public IEnumerable<ScoreDto> ScoresDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}