using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Scores.SingleMatchScores.Commands;
using CleanTableTennisApp.Application.Scores.SingleMatchScores.Queries;
using CleanTableTennisApp.Application.TeamMatches.Queries;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class ScoreEndpoint : IEndpoints
{
    private const string Tag = "Score";
    private const string BaseRoute = "score";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // todo sound weird
        app.MapGet($"{BaseRoute}/{{matchIdEncoded}}", GetScoreMatchByIdAsync)
            .WithName("GetScore")
            .Produces<ScoreDto[]>().Produces(404)
            .WithTags(Tag);

        app.MapPut($"{BaseRoute}", UpdateScoreAsync)
            .WithName("UpdateScore")
            .RequireAuthorization(Permissions.Write.Matches)
            .Accepts<UpdateSingleMatchScoreCommand>(EndpointConstants.Json)
            .Produces<bool>(200)
            // todo pending to clarify
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    private static async Task<IResult> GetScoreMatchByIdAsync(IMediator mediator, string matchIdEncoded)
    {
        var query = new GetSingleMatchScoresQuery(matchIdEncoded);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateScoreAsync(IMediator mediator, [FromBody] UpdateSingleMatchScoreCommand command)
    {
        var result = await mediator.Send(command);
        if (result)
        {
            var overviewDto = await mediator.Send(new GetTeamMatchOverviewQuery { TeamMatchIdEncoded = command.TeamMatchIdEncoded, });
            //todo pending to reactivate
            //await hubContext.Clients.All.SendAsync($"scores-matchId:{command.TeamMatchIdEncoded}", overviewDto);
        }

        return Results.Ok(result);
    }

}
