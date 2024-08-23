using MediatR;
using Zeeget.Shared.Commons.Api.CustomResponse;

namespace Zeeget.Shared.Commons.Handlers.Interfaces
{
    public interface IQuery<TResult> : IRequest<Response<TResult>> { }
}
