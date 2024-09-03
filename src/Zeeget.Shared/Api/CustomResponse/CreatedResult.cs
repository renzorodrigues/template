using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class CreatedResult : Result
    {
        public string? Data { get; }

        public CreatedResult(string? data, string message)
        {
            Data = data;
            Message = message;
            StatusCode = HttpStatusCode.Created.ToString();
            IsSuccess = true;
        }
    }
}
