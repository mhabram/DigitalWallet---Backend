using DigitalWallet.Application.Authentication.Commands.SignUp;
using DigitalWallet.Application.UnitTests.Common.Extensions;

namespace DigitalWallet.Application.UnitTests.Common.Mocks.Fakers.Authentication;
public class SignUpCommandFaker : RecordFaker<SignUpCommand>
{
    public SignUpCommandFaker()
    {
        RuleFor(s => s.FirstName, f => f.Person.FirstName);
        RuleFor(s => s.LastName, f => f.Person.LastName);
        RuleFor(s => s.UserName, f => f.Person.UserName);
        RuleFor(s => s.Email, f => f.Person.Email);
        RuleFor(s => s.CountryCode, f => f.Phone.PhoneNumber("+##"));
        RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber("#########"));
        RuleFor(s => s.Password, f => f.Internet.Password());
        RuleFor(s => s.ConfirmPassword, (_, s) => s.Password);
    }
}