using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Home.Queries;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.TeamMatches.Command;
using CleanTableTennisApp.Domain.Enums;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using WebUI.Contracts.Requests;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class TeamMatchEndpoint : IEndpoints
{
    private const string Tag = "TeamMatch";
    private const string BaseRoute = "team-match";

    public static void DefineEndpoints(IEndpointRouteBuilder app){

        app.MapPost(BaseRoute, CreateTeamMatch)
            .WithName("CreateTeamMatch")
            // todo reactivate http interceptor do not allow to see the list of team matches
            //.RequireAuthorization(Permissions.Write.Matches)
            .Accepts<CreateTeamMatchRequest>(EndpointConstants.Json)
            .Produces<string>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);

        app.MapGet($"{BaseRoute}", GetTeamMatches)
            .WithName("GetTeamMatches")
            .Produces<TeamMatchResponse[]>()
            .AllowAnonymous()
            .WithTags(Tag);
    }

    private static async Task<IResult> CreateTeamMatch(IMediator mediator, CreateTeamMatchRequest request)
    {
        var command = CreateCommand(request);

        // todo refactor returning a teammatch or teamMatchoverview
        var teamMatchId = await mediator.Send(command);
        return Results.Ok(teamMatchId);
    }

    //todo separate into mappings?
    private static CreateTeamMatchCommand CreateCommand(CreateTeamMatchRequest request)
    {
        return new()
        {
            GuestTeam = new CreateTeamPlayersDto()
            {
                Name = request.GuestTeam.Name,
                Players = request.GuestTeam.Players.Select(s => new CreatePlayerDto() { FullName = s.FullName, DoublePosition = s.DoublePosition }).ToList(),
            },
            HostTeam = new CreateTeamPlayersDto()
            {
                Name = request.HostTeam.Name,
                Players = request.HostTeam.Players.Select(s => new CreatePlayerDto() { FullName = s.FullName, DoublePosition = s.DoublePosition }).ToList(),
            },
        };
    }

    private static async Task<IResult> GetTeamMatches(IMediator mediator, string? teamMatchIdEncoded)
    {
        if (teamMatchIdEncoded is null || string.IsNullOrWhiteSpace(teamMatchIdEncoded))
        {
            var result = await mediator.Send(new GetTeamMatchesQuery());
            return Results.Ok(result.Select(s => s.ToResponse()));
        }

        var teamMatch = await mediator.Send(new GetSingleTeamMatchesQuery { TeamMatchIdEncoded = teamMatchIdEncoded });
        var response = new[] { teamMatch.ToResponse() };
        return Results.Ok(response);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add specific service for this endpoint
    }
}
