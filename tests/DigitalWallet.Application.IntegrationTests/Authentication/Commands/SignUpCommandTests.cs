using DigitalWallet.Application.Authentication.Commands.SignUp;
using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Users;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Token;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Persons;
using DigitalWallet.Application.IntegrationTests.Common.TestData;
using DigitalWallet.Application.IntegrationTests.Common.Mocks.Fakers.Authentication;
using FluentAssertions;
using DigitalWallet.Application.Common.Exceptions.Common;

namespace DigitalWallet.Application.IntegrationTests.Authentication.Commands;

public class SignUpCommandTests
{
    private readonly IJwtTokenGenerator _mockJwtTokenGenerator;
    private readonly NullLogger<SignUpCommandHandler> _nullLogger;
    private readonly CancellationToken _cancellationToken;
    private readonly SignUpCommand _command;

    public SignUpCommandTests()
    {
        _mockJwtTokenGenerator = MockJwtTokenGeneratorService.GetTokenService();
        _nullLogger = new NullLogger<SignUpCommandHandler>();
        _cancellationToken = new CancellationTokenSource().Token;
        _command = new SignUpCommandFaker().Generate();
    }

    [Fact]
    public async Task SignUpCommandReturnsSuccess()
    {
        var handler = new SignUpCommandHandler(
            MockUserCommandsRepository.GetUserCommandsRepository(),
            MockPersonCommandsRepository.GetPersonCommandsRepository(),
            _mockJwtTokenGenerator,
            _nullLogger);

        var result = await handler.Handle(_command, _cancellationToken);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Email.Should().Be(_command.Email);
        result.UserName.Should().Be(_command.UserName);
        result.Token.Should().Be(TokenTestData.GeneratedToken);
    }

    [Theory]
    [InlineData(true, false, typeof(SignUpCommandException))]
    [InlineData(false, true, typeof(ObjectExistsException))]
    [InlineData(true, true, typeof(SignUpCommandException))]
    public async Task SignUpCommandThrowsException(
        bool userException,
        bool personException,
        Type expectedType)
    {
        var handler = new SignUpCommandHandler(
            MockUserCommandsRepository.GetUserCommandsRepository(userException),
            MockPersonCommandsRepository.GetPersonCommandsRepository(personException),
            _mockJwtTokenGenerator,
            _nullLogger);

        var result = await Record.ExceptionAsync(async () =>
            await handler.Handle(_command, _cancellationToken));

        result.Should().BeOfType(expectedType);
    }
}