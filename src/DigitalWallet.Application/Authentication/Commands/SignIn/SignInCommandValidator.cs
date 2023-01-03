using FluentValidation;

namespace DigitalWallet.Application.Authentication.Commands.SignIn;

public class SignInCommandValidator
    : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress();

        RuleFor(p => p.Password)
            .NotEmpty();
    }
}