﻿using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Application.Wizard.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CleanTableTennisApp.WebUI.Controllers;

public class ScoresController : ApiControllerBase
{
    private readonly IHubContext<ScoresHub> _hubContext;

    public ScoresController(IHubContext<ScoresHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet("{matchIdEncoded}")]
    public async Task<ActionResult<ScoreDto[]>> Get(string matchIdEncoded)
    {
        var query = new GetMatchScoreQuery(matchIdEncoded);
        return await Mediator.Send(query);
    }

    [HttpPost]
    // TODO this functionality should be restricted by roles
    public async Task<ActionResult<bool>> Update([FromBody] UpdateMatchScoreCommand command)
    {
        var result = await Mediator.Send(command);
        if (result)
        {
            var overviewDto = await Mediator.Send(new GetOverviewQuery { TeamMatchIdEncoded = command.TeamMatchIdEncoded, });

            await _hubContext.Clients.All.SendAsync($"scores-matchId:{command.TeamMatchIdEncoded}", overviewDto);
        }

        return result;
    }
}