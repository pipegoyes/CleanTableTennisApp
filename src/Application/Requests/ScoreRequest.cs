namespace CleanTableTennisApp.Application.Requests;

public class ScoreRequest
{
    public string ScoreIdEncoded { get; set; } = string.Empty;
    public int HostPoints { get; set; }
    public int GuestPoints { get; set; }
}