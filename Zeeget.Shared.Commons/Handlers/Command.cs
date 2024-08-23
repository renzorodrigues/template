using System.Text.Json.Serialization;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Shared.Commons.Handlers
{
    public record Command<TResult> : ICommand<TResult>
    {
        [JsonIgnore]
        public Guid RequestId { get; set; } = Guid.NewGuid();
    }
}
