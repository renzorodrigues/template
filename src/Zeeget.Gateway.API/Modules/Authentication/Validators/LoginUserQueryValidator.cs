using FluentValidation;
using Zeeget.Gateway.API.Modules.Authentication.Dtos;

namespace Zeeget.Gateway.API.Modules.Authentication.Validators
{
    public class LoginUserQueryValidator : AbstractValidator<UserLoginDto>
    {
        public LoginUserQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is mandatory.")
                .MinimumLength(3)
                .WithMessage("Username must have at least 3 characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is mandatory.")
                .MinimumLength(6)
                .WithMessage("Username must have at least 6 characters.");
        }
    }
}
