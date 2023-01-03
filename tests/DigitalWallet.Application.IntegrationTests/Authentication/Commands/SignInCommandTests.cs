using DigitalWallet.Application.Authentication.Commands.SignIn;
using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.Common.Models;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Fakers.Authentication;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Users;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Token;
using DigitalWallet.Application.IntegrationTests.Common.TestData;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DigitalWallet.Application.IntegrationTests.Authentication.Commands;

public class SignInCommandTests
{
    private readonly IJwtTokenGenerator _mockJwtTokenGenerator;
    private readonly NullLogger<SignInCommandHandler> _nullLogger;
    private readonly CancellationToken _cancellationToken;
    private readonly SignInCommand _command;
    private readonly BaseUser _baseUser;

    public SignInCommandTests()
    {
        _mockJwtTokenGenerator = MockJwtTokenGeneratorService.GetTokenService();
        _nullLogger = new NullLogger<SignInCommandHandler>();
        _cancellationToken = new CancellationTokenSource().Token;
        _command = new SignInCommandFaker().Generate();
        _baseUser = new BaseUserFaker().Generate();
    }

    [Fact]
    public async Task SignInCommandReturnsSuccess()
    {
        var handler = new SignInCommandHandler(
            MockUserQueriesRepository.GetUserQueriesRepository(_baseUser),
            _mockJwtTokenGenerator);

        var result = await handler.Handle(_command, _cancellationToken);

        result.Should().NotBeNull();
        result.Id.Should().Be(_baseUser.Id);
        result.Email.Should().Be(_baseUser.Email);
        result.UserName.Should().Be(_baseUser.UserName);
        result.Token.Should().Be(TokenTestData.GeneratedToken);
    }

    [Fact]
    public async Task SignInCommandThrowsException()
    {
        var handler = new SignInCommandHandler(
            MockUserQueriesRepository.GetUserQueriesRepository(withException: true),
            _mockJwtTokenGenerator);

        var result = await Record.ExceptionAsync(async () =>
            await handler.Handle(_command, _cancellationToken));

        result.Should().BeOfType<SignInCommandException>();
    }
}
