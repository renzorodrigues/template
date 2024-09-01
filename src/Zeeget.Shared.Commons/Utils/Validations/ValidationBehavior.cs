using MediatR;

namespace Zeeget.Shared.Utils.Validations
{
    public class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        private readonly IEnumerable<IRequestValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IRequestValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResult> Handle(
            TRequest request,
            RequestHandlerDelegate<TResult> next,
            CancellationToken cancellationToken
        )
        {
            foreach (var validator in _validators)
            {
                validator.Validate(request);
            }

            return await next();
        }
    }
}
