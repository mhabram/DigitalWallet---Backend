using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;
using DigitalWallet.Application.Common.Models;
using DigitalWallet.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Authentication.Commands.SignUp;

public class SignUpCommandHandler
    : IRequestHandler<SignUpCommand, AuthenticationResult>
{
    private readonly ILogger<SignUpCommandHandler> _logger;
    private readonly IUserCommandsRepository _userCommandsRepository;
    private readonly IPersonCommandsRepository _personCommandsRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public SignUpCommandHandler(
        IUserCommandsRepository userCommandsRepository,
        IPersonCommandsRepository personCommandsRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<SignUpCommandHandler> logger)
    {
        _logger = logger;
        _userCommandsRepository = userCommandsRepository;
        _personCommandsRepository = personCommandsRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var user = await CreateNewUserAsync(
            request,
            cancellationToken);

        await CreateNewPersonAsync(
            request,
            cancellationToken);

        var token = _jwtTokenGenerator
            .GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.Email,
            user.UserName,
            token);
    }

    private async Task CreateNewPersonAsync(SignUpCommand request, CancellationToken cancellationToken)
    {
        var personId = Guid.NewGuid();
        var person = new Person(
            personId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.UserName,
            request.CountryCode,
            request.PhoneNumber);

        await _personCommandsRepository.CreatePersonAsync(
            person,
            cancellationToken);
    }

    private async Task<BaseUser> CreateNewUserAsync(SignUpCommand request, CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var user = new BaseUser(
            userId,
            request.UserName,
            request.Email,
            request.PhoneNumber);

        var applicationResult = await _userCommandsRepository
            .CreateUserAsync(
                user,
                request.Password,
                cancellationToken);

        if (!applicationResult.Succeeded)
        {
            foreach (var error in applicationResult.Errors)
                _logger.LogError(nameof(Handle), error);

            throw new SignUpCommandException();
        }

        return user;
    }
}