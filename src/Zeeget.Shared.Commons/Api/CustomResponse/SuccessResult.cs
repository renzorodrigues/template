using System.Net;

namespace Zeeget.Shared.Api.CustomResponse
{
    public class SuccessResult<T> : Result
    {
        public T? Data { get; }

        public SuccessResult(T? data)
        {
            Data = data;
            StatusCode = HttpStatusCode.OK.ToString();
            IsSuccess = true;
        }
    }
}
