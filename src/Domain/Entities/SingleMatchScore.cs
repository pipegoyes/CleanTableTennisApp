﻿using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class SingleMatchScore : IGamePoints
{
    // todo can I remove ? from match
    public int Id { get; set; }
    public int GamePointsHost { get; set; }
    public int GamePointsGuest { get; set; }
    public SingleMatch? Match { get; set; }
}
