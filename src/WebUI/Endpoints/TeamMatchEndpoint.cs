using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Home.Queries;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation.Results;
using MediatR;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class TeamMatchEndpoint : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "TeamMatch";
    private const string BaseRoute = "team-match";

    public static void DefineEndpoints(IEndpointRouteBuilder app){

        app.MapPost(BaseRoute, CreateTeamMatch)
            .WithName("CreateTeamMatch")
            .RequireAuthorization(Permissions.Write.Matches)
            .Accepts<CreateTeamMatchCommand>(ContentType)
            .Produces<TeamMatch>(201).Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet(BaseRoute, GetTeamMatches)
            .WithName("GetTeamMatches")
            .Produces<TeamMatchDto[]>()
            .WithTags(Tag);
    }

    private static async Task<IResult> CreateTeamMatch(IMediator mediator, CreateTeamMatchCommand command)
    {
        // todo pending to add validator
        // todo refactor returning a teammatch 
        var teamMatchId = await mediator.Send(command);
        return Results.Ok(teamMatchId);
    }

    private static async Task<IResult> GetTeamMatches(IMediator mediator, string? teamMatchIdEncoded)
    {
        if (teamMatchIdEncoded is null)
        {
            var result = await mediator.Send(new GetOnGoingTeamMatchesQuery());
            return Results.Ok(result);
        }

        var singleTeamMatchDto = await mediator.Send(new GetSingleTeamMatchesQuery { TeamMatchIdEncoded = teamMatchIdEncoded });
        var teamMatchDtos = new[] { singleTeamMatchDto };
        return Results.Ok(teamMatchDtos);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add specific service for this endpoint
    }
}
