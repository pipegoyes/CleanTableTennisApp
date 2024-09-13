using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Requests;

public class CreatePlayerDto
{
    public string FullName { get; set; } = string.Empty;
    public DoublePosition DoublePosition { get; set; }
}
