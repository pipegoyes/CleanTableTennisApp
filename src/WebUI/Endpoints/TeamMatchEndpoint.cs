using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Home.Queries;
using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class TeamMatchEndpoint : IEndpoints
{
    private const string Tag = "TeamMatch";
    private const string BaseRoute = "team-match";

    public static void DefineEndpoints(IEndpointRouteBuilder app){

        app.MapPost(BaseRoute, CreateTeamMatch)
            .WithName("CreateTeamMatch")
            .RequireAuthorization(Permissions.Write.Matches)
            .Accepts<CreateTeamMatchCommand>(EndpointConstants.Json)
            .Produces<string>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}", GetTeamMatches)
            .WithName("GetTeamMatches")
            .Produces<TeamMatchDto[]>()
            .AllowAnonymous()
            .WithTags(Tag);
    }

    private static async Task<IResult> CreateTeamMatch(IMediator mediator, CreateTeamMatchCommand command, IValidator<CreateTeamMatchCommand> validator)
    {
        await validator.ValidateAndThrowAsync(command);

        // todo refactor returning a teammatch or teamMatchoverview
        var teamMatchId = await mediator.Send(command);
        return Results.Ok(teamMatchId);
    }

    private static async Task<IResult> GetTeamMatches(IMediator mediator, string? teamMatchIdEncoded)
    {
        if (teamMatchIdEncoded is null || string.IsNullOrWhiteSpace(teamMatchIdEncoded))
        {
            var result = await mediator.Send(new GetTeamMatchesQuery());
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
