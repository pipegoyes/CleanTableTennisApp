using CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Controllers;

public class TeamMatchController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTeamMatchCommand command)
    {
        return await Mediator.Send(command);
    }
}
