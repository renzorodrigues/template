using FluentValidation;
using Zeeget.Gateway.API.Modules.Authentication.Requests;

namespace Zeeget.Gateway.API.Modules.Authentication.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleForEach(user => user.Credentials).SetValidator(new CredentialsValidator());

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("FirstName is required.");

            RuleFor(user => user.LastName).NotEmpty().WithMessage("LastName is required.");
        }
    }
}
