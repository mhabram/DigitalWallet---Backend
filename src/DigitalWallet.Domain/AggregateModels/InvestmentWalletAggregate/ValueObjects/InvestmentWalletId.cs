using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;

public sealed class InvestmentWalletId : ValueObject
{
    public Guid Value { get; private set; }

	private InvestmentWalletId(Guid value)
	{
		Value = value;
	}

	public static InvestmentWalletId CreateUnique()
	{
		return new(Guid.NewGuid());
	}

    protected override IEnumerable<object> GetEqualityComponents()
    {
		yield return Value;
    }
}
