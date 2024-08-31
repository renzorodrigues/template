namespace Zeeget.Shared.Api.CustomResponse
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message)
        {
            Message = message;
            StatusCode = System.Net.HttpStatusCode.InternalServerError.ToString();
        }
    }
}
