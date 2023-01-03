using Bogus;
using DigitalWallet.Application.Common.Models;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Fakers.Authentication;

public class BaseUserFaker : Faker<BaseUser>
{
	public BaseUserFaker()
	{
		RuleFor(b => b.Id, f => f.Random.Guid());
		RuleFor(b => b.UserName, f => f.Person.UserName);
		RuleFor(b => b.Email, f => f.Person.Email);
	}
}
