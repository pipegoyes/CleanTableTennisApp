using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewSingleMatchDto
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string HostPlayerName { get; set; } = string.Empty;
    public string GuestPlayerName { get; set; } = string.Empty;
    public int HostPoints { get; set; }
    public int GuestPoints { get; set; }
    public PlayingOrder PlayingOrder { get; set; }
    public IEnumerable<ScoreDto> ScoresDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}
