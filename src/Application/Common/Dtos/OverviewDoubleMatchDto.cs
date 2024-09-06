// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanTableTennisApp.Domain.Enums;

namespace CleanTableTennisApp.Application.Common.Dtos;

public class OverviewDoubleMatchDto
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string HostLeftPlayerName { get; set; } = string.Empty;
    public string HostRightPlayerName { get; set; } = string.Empty;
    public string GuestLeftPlayerName { get; set; } = string.Empty;
    public string GuestRightPlayerName { get; set; } = string.Empty;
    public int HostPoints { get; set; }
    public int GuestPoints { get; set; }
    public PlayingOrder PlayingOrder { get; set; }
    public IEnumerable<ScoreDto> ScoresDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}
