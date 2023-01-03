using System;
using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Authentication.Commands.SignIn;

public record SignInCommand(
    string Email,
    string Password
    ) : IRequest<AuthenticationResult>;