using CleanTableTennisApp.Application.Common.Interfaces;

namespace CleanTableTennisApp.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
