﻿using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.DoubleMatches.Commands;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Scores.DoubleMatchScores.Commands;
using CleanTableTennisApp.Application.Scores.SingleMatchScores.Commands;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using MediatR;

namespace CleanTableTennisApp.Application.TeamMatches.Command;

public class CreateTeamMatchCommand : IRequest<string>
{
    public CreateTeamPlayersDto HostTeam { get; set; } = new();
    public CreateTeamPlayersDto GuestTeam { get; set; } = new();
}

public class CreateTeamMatchHandler : IRequestHandler<CreateTeamMatchCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUrlSafeIntEncoder _urlSafeIntEncoder;

    public CreateTeamMatchHandler(IApplicationDbContext context, IMediator mediator, IUrlSafeIntEncoder urlSafeIntEncoder)
    {
        _context = context;
        _mediator = mediator;
        _urlSafeIntEncoder = urlSafeIntEncoder;
    }

    public async Task<string> Handle(CreateTeamMatchCommand request, CancellationToken cancellationToken)
    {
        var hostTeam = await AddTeamIfNotExists(request.HostTeam.Name, cancellationToken);
        AddPlayers(hostTeam, request.HostTeam.Players);

        var guestTeam = await AddTeamIfNotExists(request.GuestTeam.Name, cancellationToken);
        AddPlayers(guestTeam, request.GuestTeam.Players);

        var teamMatch = new TeamMatch(hostTeam, guestTeam);
        _context.TeamMatches.Add(teamMatch);

        await _context.SaveChangesAsync(cancellationToken);

        var createDoubleMatchesCommand = CreateDoubleMatchesCommand(request, teamMatch, guestTeam, hostTeam);

        var allSingleMatchesCreated = await _mediator.Send(new CreateSingleMatchCommand { TeamMatchId = teamMatch.Id }, cancellationToken);
        var allDoubleMatchesCreated = await _mediator.Send(createDoubleMatchesCommand, cancellationToken);
        var singleScoresCreated = await _mediator.Send(new CreateEmptySingleScoresCommand { TeamMatchId = teamMatch.Id }, cancellationToken);
        var doubleScoresCreated = await _mediator.Send(new CreateEmptyDoubleMatchScoresCommand { TeamMatchId = teamMatch.Id }, cancellationToken);

        //todo what do to with responses ?
        return _urlSafeIntEncoder.Encode(teamMatch.Id);
    }

    private static CreateDoubleMatchesCommand CreateDoubleMatchesCommand(CreateTeamMatchCommand request, TeamMatch teamMatch, Team guestTeam, Team hostTeam)
    {
        var guestNamesWithDoublePosition = request.GuestTeam.Players.ToDictionary(s => s.FullName, r => r.DoublePosition);
        var hostNamesWithDoublePosition = request.HostTeam.Players.ToDictionary(s => s.FullName, r => r.DoublePosition);

        var createDoubleMatchesCommand = new CreateDoubleMatchesCommand
        {
            TeamMatchId = teamMatch.Id,
            GuestPlayers = ToDto(guestTeam.Players, guestNamesWithDoublePosition),
            HostPlayers = ToDto(hostTeam.Players, hostNamesWithDoublePosition)
        };
        return createDoubleMatchesCommand;
    }

    private static IList<CreateDoubleMatchDto> ToDto(ICollection<Player> players, IDictionary<string, DoublePosition> namesWithDoublePosition)
    {
        return players.Select(s => new CreateDoubleMatchDto { DoublePosition = namesWithDoublePosition[s.Name], PlayerId = s.Id }).ToArray();
    }

    private static void AddPlayers(Team hostTeam, IList<CreatePlayerDto> players)
    {
        foreach (var player in players)
        {
            if (hostTeam.Players.All(s => s.Name != player.FullName))
            {
                hostTeam.Players.Add(new Player(player.FullName));
            }
        }
    }

    private async Task<Team> AddTeamIfNotExists(string teamName, CancellationToken cancellationToken)
    {
        var existingHost = _context.Teams.SingleOrDefault(s => s.Name == teamName);
        if (existingHost == null)
        {
            existingHost = new Team(teamName);
            existingHost = _context.Teams.Add(existingHost).Entity;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return existingHost;
    }
}
