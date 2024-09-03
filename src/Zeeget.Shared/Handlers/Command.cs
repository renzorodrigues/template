using System.Text.Json.Serialization;
using Zeeget.Shared.Handlers.Interfaces;

namespace Zeeget.Shared.Handlers
{
    public record Command<TResult> : ICommand<TResult>
    {
        [JsonIgnore]
        public Guid RequestId { get; set; } = Guid.NewGuid();
    }
}
