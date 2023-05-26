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