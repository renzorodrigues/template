using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class CreatedResult : Result
    {
        public Guid Data { get; }

        public CreatedResult(Guid data, string message)
        {
            Data = data;
            Message = message;
            StatusCode = HttpStatusCode.Created.ToString();
            IsSuccess = true;
        }
    }
}
