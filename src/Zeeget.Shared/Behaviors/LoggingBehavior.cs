using MediatR;
using Zeeget.Shared.Services.Logging;

public class LoggingBehavior<TRequest, TResponse>(ILoggingService loggingService) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILoggingService _loggingService = loggingService;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        _loggingService.LogRequest(request);

        var response = await next();

        _loggingService.LogResult(response);

        return response;
    }
}
