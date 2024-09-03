using MediatR;
using Zeeget.Shared.Api;

namespace Zeeget.Shared.Handlers.Interfaces
{
    public interface ICommand<TResult> : IRequest<TResult> { }
}
