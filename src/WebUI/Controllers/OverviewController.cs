using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Wizard.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CleanTableTennisApp.WebUI.Controllers;

public class OverviewController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OverviewDto>> GetAllMatches([FromQuery] GetOverviewQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> Finish([FromBody] FinishTeamMatchCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class ScoresHub : Hub
{
    // URL : /real-time-scores
}