using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Overview.Queries;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Commands;
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
    public async Task<ActionResult<bool>> Update([FromBody] UpdateDoubleMatchScoreCommand command)
    {
        return await Mediator.Send(command);
    }
}