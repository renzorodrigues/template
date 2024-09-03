using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class BadRequestResult : Result
    {
        public IDictionary<string, string[]>? Errors { get; }

        public BadRequestResult(IDictionary<string, string[]>? errors, string message)
        {
            Message = message;
            Errors = errors;
            StatusCode = HttpStatusCode.BadRequest.ToString();
        }
    }
}
