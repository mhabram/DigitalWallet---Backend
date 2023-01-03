using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Authentication.Commands.SignUp;

public record SignUpCommand(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string CountryCode,
    string PhoneNumber,
    string Password,
    string ConfirmPassword)
    : IRequest<AuthenticationResult>;