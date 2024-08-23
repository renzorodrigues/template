using MediatR;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Shared.Commons.Api.CustomResponse;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<Guid>>
    {
        public async Task<Response<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
