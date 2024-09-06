using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Wizard.Queries;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class OverviewEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Overview";
    private const string BaseRoute = "overview";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // todo belongs to teammatch? maybe an update
        app.MapPost(BaseRoute, FinishTeamMatch)
            .WithName("FinishTeamMatch")
            .RequireAuthorization(Permissions.Write.Matches)
            .Accepts<FinishTeamMatchCommand>(ContentType)
            .Produces<bool>(201).Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet(BaseRoute, GetOverviewDto)
            .WithName("GetOverviewDto")
            .Produces<OverviewDto>()
            .WithTags(Tag);
    }

    private static async Task<IResult> FinishTeamMatch(IMediator mediator, [FromBody] FinishTeamMatchCommand command)
    {
        var teamMatchId = await mediator.Send(command);
        return Results.Ok(teamMatchId);
    }

    private static async Task<IResult> GetOverviewDto(IMediator mediator, [FromBody] GetOverviewQuery query)
    {
        var result = await mediator.Send(query); ;
        return Results.Ok(result);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add specific service for this endpoint
    }
}
