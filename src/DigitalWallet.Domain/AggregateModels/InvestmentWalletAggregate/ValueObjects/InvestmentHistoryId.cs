using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;

public sealed class InvestmentHistoryId : ValueObject
{
    public Guid Value { get; private set; }

    private InvestmentHistoryId(Guid value)
    {
        Value = value;
    }

    public static InvestmentHistoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
