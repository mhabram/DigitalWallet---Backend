using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DigitalWallet.Application.Authentication.Commands.SignIn;
using DigitalWallet.Application.UnitTests.Mocks.Users;
using DigitalWallet.Application.UnitTests.Common.Extensions;
using DigitalWallet.Application.UnitTests.Common.Mocks.Fakers.Authentication;

namespace DigitalWallet.Application.UnitTests.Authentication.Commands.SignIn;

public class SignInCommandValidatorTests
{
    private readonly SignInCommand _command;
    private readonly SignInCommandValidator _validator;

    public SignInCommandValidatorTests()
    {
        _command = new SignInCommandFaker().Generate();
        _validator = new SignInCommandValidator();
    }

    [Fact]
    public async Task SignInCommandValidatorShouldSuccess()
    {
        var result = await _validator.ValidateAsync(_command);

        result.Errors.Should().HaveCount(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task SignInCommandValidatorShouldThrowEmailIsRequired(string? email)
    {
        var command = _command with { Email = email! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCountGreaterThanOrEqualTo(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task SignInCommandValidatorShouldThrowIncorrectPassword(string? password)
    {
        var command = _command with { Password = password! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(_command.Password));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("email.domain.com")]
    [InlineData("@domain.com")]
    [InlineData("email@domain@domain.com")]
    public async Task SignInCommandValidatorShouldThrowEmailDoesNotMatch(string? email)
    {
        var command = _command with { Email = email! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCountGreaterThanOrEqualTo(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }
}
