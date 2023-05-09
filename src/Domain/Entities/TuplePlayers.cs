namespace CleanTableTennisApp.Domain.Entities;

public class TuplePlayers
{
    public TuplePlayers(Player firstPlayer, Player secondPlayer)
    {
        FirstPlayer = firstPlayer;
        SecondPlayer = secondPlayer;
    }

    public Player FirstPlayer { get; set; }
    public Player SecondPlayer { get; set; }
}