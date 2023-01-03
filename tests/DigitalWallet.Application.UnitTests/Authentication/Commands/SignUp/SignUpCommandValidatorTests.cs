using System.Threading.Tasks;
using DigitalWallet.Application.Authentication.Commands.SignUp;
using DigitalWallet.Application.UnitTests.Mocks.Users;
using Xunit;
using DigitalWallet.Application.UnitTests.Common.Extensions;
using Bogus;
using FluentAssertions;
using DigitalWallet.Application.UnitTests.Common.Mocks.Fakers.Authentication;

namespace DigitalWallet.Application.UnitTests.Authentication.Commands.SignUp;

public class SignUpCommandValidatorTests
{
    private readonly SignUpCommand _command;
    private readonly SignUpCommandValidator _validator;
    private readonly Faker _faker;

    public SignUpCommandValidatorTests()
    {
        _command = new SignUpCommandFaker().Generate();
        _validator = new SignUpCommandValidator(MockUserQueriesRepository.GetQueriesRepository());
        _faker = new Faker();
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldSuccess()
    {
        var result = await _validator.ValidateAsync(_command);

        result.Errors.Should().HaveCount(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task SignUpCommandValidatorShouldThrowUserNameIsRequired(string? userName)
    {
        var command = _command with { UserName = userName! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.UserName));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowUserNameMaximumLength()
    {
        var tooLongName = _faker.Random.String2(length: 51);
        var command = _command with { UserName = tooLongName };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.UserName));
    }

    [Fact]
    public async Task SingUpCommandValidatorShouldThrowFirstNameMaximumLength()
    {
        var tooLongName = _faker.Random.String2(length: 51);
        var command = _command with { FirstName = tooLongName };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.FirstName));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowLastNameMaximumLength()
    {
        var tooLongLastName = _faker.Random.String2(length: 51);
        var command = _command with { LastName = tooLongLastName };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.LastName));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task SignUpCommandValidatorShouldThrowEmailIsRequired(string? email)
    {
        var command = _command with { Email = email! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCountGreaterThanOrEqualTo(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("email.domain.com")]
    [InlineData("@domain.com")]
    [InlineData("email@domain@domain.com")]
    public async Task SignUpCommandValidatorShouldThrowEmailDoesNotMatch(string? email)
    {
        var command = _command with { Email = email! };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCountGreaterThanOrEqualTo(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowEmailMaximumLength()
    {
        var tooLongEmail = _faker.Random.String2(length: 51);
        var command = _command with { Email = tooLongEmail };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCountGreaterThanOrEqualTo(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowUniqueEmailIsRequired()
    {
        var command = _command;
        var validator = new SignUpCommandValidator(MockUserQueriesRepository.GetQueriesRepository(false));

        var result = await validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.Email));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowCountryCodeMaximumLength()
    {
        var command = _command with { CountryCode = "123456" };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.CountryCode));
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowPhoneNumberMaximumLength()
    {
        var command = _command with { PhoneNumber = "12345676890123456" };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.PhoneNumber));
    }

    [Theory]
    [InlineData("PhoneNumber")]
    [InlineData("asd-123-asd-2")]
    [InlineData("9123hn8923")]
    public async Task SignUpCommandValidatorShouldThrowInvalidPhoneNumber(string phoneNumber)
    {
        var command = _command with { PhoneNumber = phoneNumber };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.PhoneNumber));
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("123456789012345")]
    public async Task SignUpCommandValidatorShouldSuccessPhoneNumber(string phoneNumber)
    {
        var command = _command with { PhoneNumber = phoneNumber };

        var result = await _validator.ValidateAsync(command);

        result.Errors.Should().HaveCount(0);
    }

    [Fact]
    public async Task SignUpCommandValidatorShouldThrowPasswordDoesNotMatch()
    {
        var command = _command with { Password = "otherPassword" };

        var result = await _validator.ValidateAsync(command);

        result.Errors?.Should().HaveCount(1);
        result.Errors?.AssertPropertyName(nameof(command.Password));
    }
}