namespace CleanTableTennisApp.Application.Common.Dtos;

public class ScoreDto
{
    public string ScoreIdEncoded { get; set; } = string.Empty;
    public int HostPoints { get; set; }
    public int GuestPoints { get; set; }
}
// todo exclusive model for signalR ?
//public class ScoreDto
//{
//    public string ScoreIdEncoded { get; set; } = string.Empty;
//    public int HostPoints { get; set; }
//    public int GuestPoints { get; set; }
//}

