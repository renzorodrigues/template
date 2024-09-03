using MediatR;
using Microsoft.Extensions.Logging;
using Zeeget.Shared.Services.Logging;
using static Zeeget.Shared.Utils.Constants.ContantStrings;

public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;
    private Type? _request;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogRequest<TRequest>(TRequest request)
    {
        _request = typeof(TRequest);    
        _logger.LogInformation(LoggingMessages.Handling, _request);
    }

    public void LogResult<TResponse>(TResponse response)
    {
        _logger.LogInformation(LoggingMessages.Handled, _request);
    }
}
