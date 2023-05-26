namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewSingleMatchDto
{
    public int MatchId { get; set; }
    public string HostPlayerName { get; set; } = string.Empty;
    public string GuestPlayerName { get; set; } = string.Empty;
    public int HostPoints { get; set; } = 0;
    public int GuestPoints { get; set; } = 0;
}