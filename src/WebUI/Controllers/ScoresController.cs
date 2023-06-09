using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class ScoresController : ApiControllerBase
{
    [HttpGet("{matchIdEncoded}")]
    public async Task<ActionResult<ScoreDto[]>> Get(string matchIdEncoded)
    {
        var query = new GetMatchScoreQuery(matchIdEncoded);
        return await Mediator.Send(query);
    }


    [HttpPost]
    public async Task<ActionResult<bool>> Update([FromBody] UpdateMatchScoreCommand command)
    {
        return await Mediator.Send(command);
    }
}