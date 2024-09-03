using MediatR;
using Zeeget.Shared.Api;

namespace Zeeget.Shared.Commons.Handlers.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
