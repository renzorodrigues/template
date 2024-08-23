using MediatR;
using Zeeget.Shared.Commons.Api.CustomResponse;

namespace Zeeget.Shared.Commons.Handlers.Interfaces
{
    public interface ICommand<TResult> : IRequest<Response<TResult>> { }
}
