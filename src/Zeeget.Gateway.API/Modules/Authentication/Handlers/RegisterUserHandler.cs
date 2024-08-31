using MediatR;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Shared.Api;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        public Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
