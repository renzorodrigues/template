namespace Zeeget.Shared.Api.CustomResponse
{
    public class ConflictResult : Result
    {
        public ConflictResult(string message)
        {
            Message = message;
            StatusCode = System.Net.HttpStatusCode.Conflict.ToString();
        }
    }
}
