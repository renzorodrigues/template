namespace Zeeget.Shared.Exceptions
{
    public class CustomValidationException(IDictionary<string, string[]> errors) : Exception
    {
        public IDictionary<string, string[]> Errors { get; } = errors;
    }
}
