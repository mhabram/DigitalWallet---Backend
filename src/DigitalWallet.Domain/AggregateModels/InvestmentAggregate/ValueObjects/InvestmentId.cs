using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.InvestmentAggregate.ValueObjects;

public sealed class InvestmentId : ValueObject
{
    public Guid Value { get; private set; }

    private InvestmentId(Guid value)
    {
        Value = value;
    }

    public static InvestmentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
