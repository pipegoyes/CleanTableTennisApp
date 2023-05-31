using CleanTableTennisApp.Application.Wizard.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class TeamMatchController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Create([FromBody] CreateTeamMatchCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class ScoresController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> Update([FromBody] UpdateMatchScoreCommand command)
    {
        return await Mediator.Send(command);
    }
}