namespace CleanTableTennisApp.Application.Common.Dtos;

public class TeamMatchOverviewDto
{
    public IReadOnlyCollection<OverviewSingleMatchDto> OverviewSingleMatchDtos { get; set; } = new List<OverviewSingleMatchDto>();
    public IReadOnlyCollection<OverviewDoubleMatchDto> OverviewDoubleMatchDtos { get; set; } = new List<OverviewDoubleMatchDto>();
}
