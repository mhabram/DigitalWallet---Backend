using DigitalWallet.Application.Authentication.Commands.SignIn;
using DigitalWallet.Application.UnitTests.Common.Extensions;

namespace DigitalWallet.Application.UnitTests.Common.Mocks.Fakers.Authentication;
public class SignInCommandFaker : RecordFaker<SignInCommand>
{
    public SignInCommandFaker()
    {
        RuleFor(s => s.Email, f => f.Person.Email);
        RuleFor(s => s.Password, f => f.Internet.Password());
    }
}
