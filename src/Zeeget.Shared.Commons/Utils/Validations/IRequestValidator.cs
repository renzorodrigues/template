namespace Zeeget.Shared.Utils.Validations
{
    public interface IRequestValidator<TRequest>
    {
        void Validate(TRequest request);
    }
}
