using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;

public sealed class WalletHistoryId : ValueObject
{
    public Guid Value { get; private set; }

	private WalletHistoryId(Guid value)
	{
		Value = value;
	}

	public static WalletHistoryId CreateUnique()
	{
		return new(Guid.NewGuid());
	}

    protected override IEnumerable<object> GetEqualityComponents()
    {
		yield return Value;
    }
}
