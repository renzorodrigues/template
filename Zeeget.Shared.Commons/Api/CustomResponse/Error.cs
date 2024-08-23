namespace Zeeget.Shared.Commons.Api.CustomResponse
{
    public class Error(string errorMessage)
    {
        public string Message { get; } = errorMessage;
    }
}
