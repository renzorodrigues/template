using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Zeeget.Shared.Commons.Api.CustomResponse;
using Zeeget.Shared.Commons.Utils.Validations;

namespace Zeeget.Shared.Commons.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            ValidationException.Errors.Clear();

            await next(context);
        }
        catch (ValidationException ex)
        {
            var result = new CustomResponse()
            {
                StatusCode = ValidationException.StatusCode,
                Errors = ValidationException.Errors,
                Message = ex.Message,
                IsSuccess = false,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)result.StatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (HttpRequestException ex)
        {
            HttpStatusCode statusCode = ex.StatusCode is null
                ? HttpStatusCode.InternalServerError
                : ex.StatusCode.Value;

            if (ex.StatusCode is null && ex.Message == HttpStatusCode.Unauthorized.ToString())
            {
                statusCode = HttpStatusCode.Unauthorized;
            }

            var result = new CustomResponse()
            {
                StatusCode = statusCode,
                Message = ex.Message,
                IsSuccess = false,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)result.StatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        catch (Exception ex)
        {
            var exceptionErrorMessage = ex.InnerException is null
                ? ex.StackTrace ?? "No Strack Trace"
                : $"{ex.InnerException.Message} - Source: {ex.InnerException.Source}";

            var result = new CustomResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Errors = [new(exceptionErrorMessage)],
                Message = ex.Message,
                IsSuccess = false,
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}
