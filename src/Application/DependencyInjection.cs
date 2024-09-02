using System.Reflection;
using CleanTableTennisApp.Application.Common.Behaviours;
using CleanTableTennisApp.Application.Common.Converters;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Overview;
using CleanTableTennisApp.Application.Scores;
using CleanTableTennisApp.Application.Scores.Validators;
using CleanTableTennisApp.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTableTennisApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddSingleton<IUrlSafeIntEncoder, UrlSafeIntEncoder>();
        services.AddSingleton<IValidator<ICollection<Score>>, ScoreValidator>();
        services.AddSingleton<IValidator<ICollection<DoubleMatchScore>>, DoubleScoreValidator>();
        services.AddSingleton<IVictoriesCounter, VictoriesCounter>();

        services.AddTransient<IGamePointsUpdater<Score>, GamePointsUpdater<Score>>();
        services.AddTransient<IGamePointsUpdater<DoubleMatchScore>, GamePointsUpdater<DoubleMatchScore>>();

        services.AddSingleton<ITeamMatchConverter, TeamMatchConverter>();
        services.AddSingleton<IScoreDtoConverter, ScoreDtoConverter>();
        services.AddSingleton<ITeamMatchVictoriesCounter, TeamMatchVictoriesCounter>();

        return services;
    }
}
