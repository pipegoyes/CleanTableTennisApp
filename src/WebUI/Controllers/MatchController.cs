using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Wizard.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class MatchController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OverviewDto>> GetAllMatches([FromQuery] GetMatchesQuery query)
    {
        return await Mediator.Send(query);
    }
}