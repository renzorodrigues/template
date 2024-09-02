using FluentValidation;
using Zeeget.Gateway.API.Modules.Authentication.Requests;

namespace Zeeget.Gateway.API.Modules.Authentication.Validators
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.User.Username)
                .NotEmpty()
                .WithMessage("Username is mandatory.")
                .MinimumLength(3)
                .WithMessage("Username must have at least 3 characters.");

            RuleFor(x => x.User.Password)
                .NotEmpty()
                .WithMessage("Password is mandatory.")
                .MinimumLength(6)
                .WithMessage("Username must have at least 6 characters.");
        }
    }
}
