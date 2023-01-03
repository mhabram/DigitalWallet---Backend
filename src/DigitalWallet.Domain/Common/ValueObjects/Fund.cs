using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.Common.ValueObjects;

public sealed class Fund : ValueObject
{
    public double Amount { get; }
    public string Currency { get; }

    private Fund(
        double amount,
        string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Fund Create(
        double amount = 0,
        string currency = "")
    {
        return new(
            amount,
            currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}
