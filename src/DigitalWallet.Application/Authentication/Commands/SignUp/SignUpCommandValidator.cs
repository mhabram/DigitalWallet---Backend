using System.Data;
using System.Text.RegularExpressions;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Domain.Constants;
using FluentValidation;

namespace DigitalWallet.Application.Authentication.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private readonly IUserQueriesRepository _userQueriesRepository;

    public SignUpCommandValidator(
        IUserQueriesRepository userQueriesRepository)
    {
        _userQueriesRepository = userQueriesRepository;

        RuleFor(s => s.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(s => s.FirstName)
            .MaximumLength(50);

        RuleFor(s => s.LastName)
            .MaximumLength(50);

        RuleFor(s => s.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress()
            .MustAsync(IsEmailUnique)
                .WithMessage(ErrorMessages.ExceptionMessages.EmailUnique);

        RuleFor(s => s.CountryCode)
            .MaximumLength(5);

        RuleFor(s => s.PhoneNumber)
            .MaximumLength(15)
            .MustAsync(IsPhoneNumber)
                .WithMessage(ErrorMessages.ExceptionMessages.PhoneNumberDoesNotMatch);

        RuleFor(s => s.Password)
            .NotEmpty()
            .Equal(s => s.ConfirmPassword)
                .WithMessage(ErrorMessages.ExceptionMessages.PasswordDoesNotMatch);
    }

    private Task<bool> IsPhoneNumber(string number, CancellationToken cancellationToken)
    {
        if (number.All(n => Char.IsDigit(n)))
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    private async Task<bool> IsEmailUnique(SignUpCommand signUpCommand, string email, CancellationToken cancellationToken)
    {
        return await _userQueriesRepository.IsEmailUniqueAsync(email, cancellationToken);
    }
}