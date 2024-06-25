using CleanTableTennisApp.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CleanTableTennisApp.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        string userName = string.Empty;

        //if (!string.IsNullOrEmpty(userId))
        //{
        //    userName = await _identityService.GetUserNameAsync(userId);
        //}

        _logger.LogInformation("CleanTableTennisApp Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
        return Task.CompletedTask;
    }
}
