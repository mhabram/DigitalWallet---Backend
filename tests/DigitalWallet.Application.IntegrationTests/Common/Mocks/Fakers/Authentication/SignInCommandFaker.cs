using DigitalWallet.Application.Authentication.Commands.SignIn;
using DigitalWallet.Application.IntegrationTests.Common.Extensions;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Fakers.Authentication;

public class SignInCommandFaker : RecordFaker<SignInCommand>
{
    public SignInCommandFaker()
    {
        RuleFor(s => s.Email, f => f.Person.Email);
        RuleFor(s => s.Password, f => f.Internet.Password());
    }
}
