using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class NotFoundResult : Result
    {
        public NotFoundResult(string message)
        {
            Message = message;
            StatusCode = HttpStatusCode.NotFound.ToString();
        }
    }
}
