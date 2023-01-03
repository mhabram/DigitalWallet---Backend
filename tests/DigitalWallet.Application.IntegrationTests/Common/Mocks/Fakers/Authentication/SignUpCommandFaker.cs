using DigitalWallet.Application.Authentication.Commands.SignUp;
using DigitalWallet.Application.IntegrationTests.Common.Extensions;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Fakers.Authentication;

public class SignUpCommandFaker : RecordFaker<SignUpCommand>
{
	public SignUpCommandFaker()
	{
		RuleFor(s => s.UserName, f => f.Person.UserName);
		RuleFor(s => s.Email, f => f.Person.Email);
		RuleFor(s => s.Password, f => f.Internet.Password());
		RuleFor(s => s.ConfirmPassword, (_, s) => s.Password);
    }
}
