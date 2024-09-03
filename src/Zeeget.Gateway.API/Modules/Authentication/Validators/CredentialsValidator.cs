using FluentValidation;
using Zeeget.Gateway.API.Modules.Authentication.Models;

namespace Zeeget.Gateway.API.Modules.Authentication.Validators
{
    public class CredentialsValidator : AbstractValidator<Credential>
    {
        public CredentialsValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must contain at least one special character.");
        }
    }
}
