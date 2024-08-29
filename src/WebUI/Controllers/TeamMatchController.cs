using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Home.Queries;
using CleanTableTennisApp.Application.Wizard.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class TeamMatchController : ApiControllerBase
{
    [HttpPost]
    [Authorize(Permissions.Write.Matches)]
    public async Task<ActionResult<string>> Create([FromBody] CreateTeamMatchCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<TeamMatchDto[]>> Get()
    {
        return await Mediator.Send(new GetOnGoingTeamMatchesQuery());
    }

    [HttpGet]
    [Route("singleTeamMatch")]
    public async Task<ActionResult<TeamMatchDto>> GetSingle([FromQuery] string teamMatchIdEncoded)
    {
        return await Mediator.Send(new GetSingleTeamMatchesQuery { TeamMatchIdEncoded = teamMatchIdEncoded });
    }
}
