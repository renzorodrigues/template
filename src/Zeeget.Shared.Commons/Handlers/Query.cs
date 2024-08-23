using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Shared.Commons.Handlers
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        public Guid RequestId { get; set; }
    }
}
