namespace Zeeget.Shared.Services.Logging
{
    public interface ILoggingService
    {
        void LogRequest<TRequest>(TRequest request);
        void LogResult<TResult>(TResult result);
    }
}
