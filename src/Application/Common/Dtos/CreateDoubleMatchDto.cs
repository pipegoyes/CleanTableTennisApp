using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Requests;

public class CreateDoubleMatchDto
{
    public int PlayerId { get; set; }
    public DoublePosition DoublePosition { get; set; }
}
