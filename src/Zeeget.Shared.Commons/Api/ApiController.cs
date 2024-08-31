using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zeeget.Shared.Commons.Handlers.Interfaces;
using Zeeget.Shared.Handlers.Interfaces;
using CreatedResult = Zeeget.Shared.Api.CustomResponse.CreatedResult;
using NotFoundResult = Zeeget.Shared.Api.CustomResponse.NotFoundResult;
using UnauthorizedResult = Zeeget.Shared.Api.CustomResponse.UnauthorizedResult;

namespace Zeeget.Shared.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController(ILogger<ApiController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<ApiController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        // QUERIES
        protected async Task<IActionResult> ExecuteQueryAsync<TRequest>(
            TRequest query,
            CancellationToken cancellationToken
        )
            where TRequest : class, IQuery<Result>
        {
            _logger.LogInformation($"{nameof(ExecuteQueryAsync)}: {typeof(TRequest)}");
            return await ExecuteAsync(query, cancellationToken);
        }

        // COMMANDS
        protected async Task<IActionResult> ExecuteCommandAsync<TRequest>(
            TRequest command,
            CancellationToken cancellationToken
        )
            where TRequest : class, ICommand<Result>
        {
            return await ExecuteAsync(command, cancellationToken);
        }

        private async Task<IActionResult> ExecuteAsync<TRequest>(
            TRequest request,
            CancellationToken cancellationToken
        )
            where TRequest : class, IRequest<Result>
        {
            IActionResult actionResult;

            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsSuccess)
            {
                actionResult = result switch
                {
                    CreatedResult createdResult
                        => Created($"{Request.Path.Value}{createdResult.Data}", result),
                    _ => Ok(result)
                };
            }
            else
            {
                actionResult = result switch
                {
                    NotFoundResult notFoundResult => NotFound(notFoundResult.Message),
                    UnauthorizedResult unauthorizedResult => Unauthorized(unauthorizedResult.Message),
                    _ => BadRequest(result)
                };
            }

            _logger.LogInformation($"{nameof(ExecuteQueryAsync)}: {typeof(TRequest)} :: {result.StatusCode}");

            return actionResult;
        }
    }
}
