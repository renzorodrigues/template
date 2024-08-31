using System.Net;

namespace Zeeget.Shared.Utils.Validations
{
    public class ValidationException(string message) : Exception(message)
    {
        public static IList<string> Errors { get; set; } = [];
        public static HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}
