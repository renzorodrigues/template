using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Zeeget.Shared.Commons.Api.CustomResponse;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Shared.Commons.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController(ILogger<ApiController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<ApiController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        // QUERIES
        protected async Task<IActionResult> ExecuteQueryAsync<TRequest, TResult>(
            TRequest query,
            CancellationToken cancellationToken
        )
            where TRequest : class, IQuery<TResult>
        {
            return await ExecuteAsync<TRequest, TResult>(query, cancellationToken);
        }

        protected async Task<IActionResult> ExecuteQueryAsync<TRequest>()
            where TRequest : class, IRequest, new()
        {
            return await ExecuteAsync(new TRequest());
        }

        // COMMANDS
        protected async Task<IActionResult> ExecuteCommandAsync<TRequest, TResponse>(
            TRequest command,
            CancellationToken cancellationToken
        )
            where TRequest : class, ICommand<TResponse>
        {
            return await ExecuteAsync<TRequest, TResponse>(command, cancellationToken);
        }

        private async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(
            TRequest request,
            CancellationToken cancellationToken
        )
            where TRequest : class, IRequest<Response<TResponse>>
        {
            IActionResult actionResult;

            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsSuccess)
            {
                actionResult = result.StatusCode switch
                {
                    HttpStatusCode.Created => Created($"{Request.Path.Value}{result.Data}", result),
                    HttpStatusCode.NoContent => NoContent(),
                    _ => Ok(result)
                };
            }
            else
            {
                actionResult = result.StatusCode switch
                {
                    HttpStatusCode.NotFound => NotFound(result),
                    HttpStatusCode.Unauthorized => Unauthorized(result),
                    _ => BadRequest(result)
                };
            }

            return actionResult;
        }

        private async Task<IActionResult> ExecuteAsync<TRequest>(TRequest request)
            where TRequest : class, IRequest
        {
            await _mediator.Send(request);

            return NoContent();
        }
    }
}
