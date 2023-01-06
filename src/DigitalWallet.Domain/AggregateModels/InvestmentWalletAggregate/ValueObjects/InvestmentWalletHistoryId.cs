using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;

public sealed class InvestmentWalletHistoryId : ValueObject
{
    public Guid Value { get; private set; }

    private InvestmentWalletHistoryId(Guid value)
    {
        Value = value;
    }

    public static InvestmentWalletHistoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
