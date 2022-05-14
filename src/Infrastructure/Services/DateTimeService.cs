using CleanArchitectureSolution.Application.Common.Interfaces;

namespace CleanArchitectureSolution.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
