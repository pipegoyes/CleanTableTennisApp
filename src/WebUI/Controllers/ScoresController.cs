using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Application.Wizard.Queries;
using CleanTableTennisApp.WebUI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CleanTableTennisApp.WebUI.Controllers;

public class ScoresController(IHubContext<ScoresHub> hubContext) : ApiControllerBase
{
    [HttpGet("{matchIdEncoded}")]
    public async Task<ActionResult<ScoreDto[]>> Get(string matchIdEncoded)
    {
        var query = new GetMatchScoreQuery(matchIdEncoded);
        return await Mediator.Send(query);
    }

    [HttpPost]
    [Authorize(Permissions.Write.Matches)]
    public async Task<ActionResult<bool>> Update([FromBody] UpdateMatchScoreCommand command)
    {
        var result = await Mediator.Send(command);
        if (result)
        {
            var overviewDto = await Mediator.Send(new GetOverviewQuery { TeamMatchIdEncoded = command.TeamMatchIdEncoded, });

            await hubContext.Clients.All.SendAsync($"scores-matchId:{command.TeamMatchIdEncoded}", overviewDto);
        }

        return result;
    }
}
