using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.Application.TeamMatches.Queries;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            .Produces<bool>(200, ContentType)
            //.Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}/{{teamMatchIdEncoded}}", GetOverviewDto)
            .WithName("GetOverviewDto")
            .Produces<TeamMatchOverviewDto>()
            .WithTags(Tag);
    }

    private static async Task<IResult> FinishTeamMatch(IMediator mediator, [FromBody] FinishTeamMatchCommand command)
    {
        var teamMatchId = await mediator.Send(command);
        return Results.Ok<bool>(teamMatchId);
    }

    private static async Task<IResult> GetOverviewDto(IMediator mediator, string teamMatchIdEncoded)
    {
        var query = new GetTeamMatchOverviewQuery { TeamMatchIdEncoded = teamMatchIdEncoded };
        var result = await mediator.Send(query); ;
        return Results.Ok(result);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add specific service for this endpoint
    }
}
