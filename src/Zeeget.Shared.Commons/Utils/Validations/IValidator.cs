namespace Zeeget.Shared.Utils.Validations
{
    public interface IValidator
    {
        void Failure(string errorMessage);
        void ThrowException();
        void CheckError();
    }
}
