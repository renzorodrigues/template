using FluentValidation;
using MediatR;
using Zeeget.Shared.Exceptions;

namespace Zeeget.Shared.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators
    ) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (validationResults.Count != 0)
                {
                    var errors = validationResults
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            failureGroup => failureGroup.Key.ToLower(),
                            failureGroup => failureGroup.Select(e => e.ErrorMessage).ToArray()
                        );

                    throw new CustomValidationException(errors);
                }
            }

            return await next();
        }
    }
}
