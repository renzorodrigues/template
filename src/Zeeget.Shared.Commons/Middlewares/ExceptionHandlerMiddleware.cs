using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Zeeget.Shared.Api;

namespace Zeeget.Shared.Middlewares
{
    public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, IResult result) : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;
        private readonly IResult _result = result;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpRequestException ex)
            {
                Result result;

                if (ex.Message == HttpStatusCode.Unauthorized.ToString())
                {
                    result = _result.Unauthorized();
                    _logger.LogWarning($"HttpRequestException: {result.Message} :: {result.StatusCode}");
                }
                else
                {
                    result = _result.Error(ex.Message);
                    _logger.LogError($"HttpRequestException: {result.Message} :: {result.StatusCode}");
                }

                _ = Enum.TryParse(result.StatusCode, out HttpStatusCode statusCode);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
            catch (Exception ex)
            {
                var exceptionErrorMessage = ex.InnerException is null
                    ? ex.StackTrace ?? "No Strack Trace"
                    : $"{ex.InnerException.Message} - Source: {ex.InnerException.Source}";

                var result = _result.Error(exceptionErrorMessage);

                _ = Enum.TryParse(result.StatusCode, out HttpStatusCode statusCode);

                _logger.LogError($"HttpRequestException: {result.Message} :: {result.StatusCode}");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
