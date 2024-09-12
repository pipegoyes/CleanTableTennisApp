using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Overview.Commands;
using CleanTableTennisApp.Application.Overview.Queries;
using CleanTableTennisApp.WebUI.Endpoints.Internal;
using CleanTableTennisApp.WebUI.Permission;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTableTennisApp.WebUI.Endpoints;

public class DoubleScoreEndpoint : IEndpoints
{
    private const string Tag = "DoubleScore";
    private const string BaseRoute = "double-score";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // todo sound weird
        app.MapGet($"{BaseRoute}/{{doubleMatchIdEncoded}}", GetDoubleMatchByIdAsync)
            .WithName("GetDoubleScore")
            .Produces<ScoreDto[]>().Produces(404)
            .WithTags(Tag);

        app.MapPut($"{BaseRoute}", UpdateDoubleScoreAsync)
            .WithName("UpdateDoubleScore")
            .RequireAuthorization(Permissions.Write.Matches)
            .Accepts<UpdateDoubleMatchScoreCommand>(EndpointConstants.ContentType)
            .Produces<bool>(200).Produces<IEnumerable<ValidationFailure>>(400)
            .WithTags(Tag);
    }

    private static async Task<IResult> GetDoubleMatchByIdAsync(IMediator mediator, string doubleMatchIdEncoded)
    {
        var query = new GetDoubleMatchScoreQuery(doubleMatchIdEncoded);
        var result = await mediator.Send(query);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateDoubleScoreAsync(IMediator mediator, [FromBody] UpdateDoubleMatchScoreCommand command)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add services if required
    }
}
