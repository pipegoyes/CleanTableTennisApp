namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewDto
{
    public IReadOnlyCollection<OverviewSingleMatchDto> OverviewSingleMatchDtos { get; set; } = new List<OverviewSingleMatchDto>();
    public IReadOnlyCollection<OverviewDoubleMatchDto> OverviewDoubleMatchDtos { get; set; } = new List<OverviewDoubleMatchDto>();
}