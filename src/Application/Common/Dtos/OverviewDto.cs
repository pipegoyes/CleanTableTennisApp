namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewDto
{
    public IList<OverviewSingleMatchDto> OverviewSingleMatchDtos { get; set; } = new List<OverviewSingleMatchDto>();
    public IList<OverviewDoubleMatchDto> OverviewDoubleMatchDtos { get; set; } = new List<OverviewDoubleMatchDto>();
}