using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class BadRequestResult : Result
    {
        public BadRequestResult(string message)
        {
            Message = message;
            StatusCode = HttpStatusCode.BadRequest.ToString();
        }
    }
}
