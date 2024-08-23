using System.Net;

namespace Zeeget.Shared.Commons.Api.CustomResponse
{
    public class Response<T> : CustomResponse
    {
        public T? Data { get; set; }
        public IEnumerable<Error>? Errors { get; set; }

        public static Response<T> RequestOk(
            T data,
            string message = "Request Successfully!",
            HttpStatusCode statusCode = HttpStatusCode.OK
        )
        {
            return new()
            {
                Data = data,
                Message = message,
                IsSuccess = true,
                StatusCode = statusCode,
            };
        }

        public static Response<T> RequestFailed(
            string message = "Request Failed!",
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            IEnumerable<Error>? errors = null
        )
        {
            return new()
            {
                Message = message,
                Errors = errors,
                IsSuccess = false,
                StatusCode = statusCode,
            };
        }
    }
}
