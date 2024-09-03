namespace Zeeget.Shared.Api.CustomResponse
{
    public class UnauthorizedResult : Result
    {
        public UnauthorizedResult(string message)
        {
            Message = message;
            StatusCode = System.Net.HttpStatusCode.Unauthorized.ToString();
        }
    }
}
