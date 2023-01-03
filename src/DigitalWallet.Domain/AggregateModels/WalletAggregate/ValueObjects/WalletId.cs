using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;

public sealed class WalletId : ValueObject
{
    public Guid Value { get; private set; }

	private WalletId(Guid value)
	{
		Value = value;
	}

	public static WalletId CreateUnique()
	{
		return new(Guid.NewGuid());
	}

    protected override IEnumerable<object> GetEqualityComponents()
    {
		yield return Value;
    }
}
