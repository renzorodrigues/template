namespace Zeeget.Shared.Commons.Utils.Validations
{
    public class ValidationRequest : IValidator
    {
        public void Failure(string errorMessage) =>
            ValidationException.Errors.Add(new(errorMessage));

        public void ThrowException() =>
            throw new ValidationException($"{nameof(ValidationException)}");

        public void CheckError()
        {
            if (ValidationException.Errors.Any())
            {
                ThrowException();
            }
        }
    }
}
