using Microsoft.Extensions.Logging;
using Zeeget.Shared.Services.Logging;

public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogRequest<TRequest>(TRequest request)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");
    }

    public void LogResult<TResponse>(TResponse response)
    {
        _logger.LogInformation($"Handled {typeof(TResponse).Name}");
    }
}
