using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Zeeget.Shared.Api;
using Zeeget.Shared.Exceptions;
using static Zeeget.Shared.Utils.Constants.ContantStrings;

namespace Zeeget.Shared.Middlewares
{
    public class ExceptionHandlerMiddleware(
        ILogger<ExceptionHandlerMiddleware> logger,
        IResult result
    ) : IMiddleware
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
                    _logger.LogWarning(
                        LoggingMessages.Exception,
                        nameof(HttpRequestException),
                        result.Message,
                        result.StatusCode
                    );
                }
                else
                {
                    result = _result.Error(ex.Message);
                    _logger.LogWarning(
                        LoggingMessages.Exception,
                        nameof(HttpRequestException),
                        result.Message,
                        result.StatusCode
                    );
                }

                _ = Enum.TryParse(result.StatusCode, out HttpStatusCode statusCode);

                await WriteAsyn(context, statusCode, result);
            }
            catch (CustomValidationException ex)
            {
                var result = _result.BadRequest(ex.Errors);

                _logger.LogWarning(
                    LoggingMessages.Exception,
                    nameof(CustomValidationException),
                    result.Message,
                    result.StatusCode
                );

                await WriteAsyn(context, HttpStatusCode.BadRequest, result);
            }
            catch (Exception ex)
            {
                var exceptionErrorMessage = ex.InnerException is null
                    ? ex.StackTrace ?? "No Strack Trace"
                    : $"{ex.InnerException.Message} - Source: {ex.InnerException.Source}";

                var result = _result.Error(exceptionErrorMessage);

                _ = Enum.TryParse(result.StatusCode, out HttpStatusCode statusCode);

                _logger.LogWarning(
                    LoggingMessages.Exception,
                    nameof(Exception),
                    result.Message,
                    result.StatusCode
                );

                await WriteAsyn(context, statusCode, result);
            }
        }

        private static async Task WriteAsyn<TResult>(
            HttpContext context,
            HttpStatusCode statusCode,
            TResult result
        )
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
