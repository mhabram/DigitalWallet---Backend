using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Application.Common.Models;
using MediatR;

namespace DigitalWallet.Application.Authentication.Commands.SignIn;

public class SignInCommandHandler
    : IRequestHandler<SignInCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserQueriesRepository _userQueriesRepository;
    
    public SignInCommandHandler(
        IUserQueriesRepository userQueriesRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userQueriesRepository = userQueriesRepository;
    }

    public async Task<AuthenticationResult> Handle(
        SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userQueriesRepository.SignInAsync(command.Email, command.Password, cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user.Id,
            user.Email,
            user.UserName,
            token);
    }
}
