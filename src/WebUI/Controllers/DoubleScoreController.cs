using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Overview.Queries;
using CleanTableTennisApp.WebUI.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class DoubleScoreController : ApiControllerBase
{
    [HttpGet("{doubleMatchIdEncoded}")]
    public async Task<ActionResult<ScoreDto[]>> Get(string doubleMatchIdEncoded)
    {
        var query = new GetDoubleMatchScoreQuery(doubleMatchIdEncoded);
        return await Mediator.Send(query);
    }

    [HttpPost]
    [Authorize(Permissions.Write.Matches)]
    public async Task<ActionResult<bool>> Update([FromBody] UpdateDoubleMatchScoreCommand command)
    {
        return await Mediator.Send(command);
    }
}
