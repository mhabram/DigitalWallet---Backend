using DigitalWallet.Domain.AggregateModels.InvestmentAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;
using DigitalWallet.Domain.Common.ValueObjects;

namespace DigitalWallet.Domain.AggregateModels.InvestmentAggregate;

public sealed class Investment : IAuditableEntity
{
    public InvestmentId Id { get; private set; }
    public double Price { get; private set; }
    public string Currency { get; private set; }
    public int Quantity { get; private set; }
    public string Category { get; private set; }
    public DateTime DateTimeStart { get; private set; }
    public DateTime? DateTimeEnd { get; private set; }
    public InvestmentWalletId InvestmentWalletId { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    public Investment(
        InvestmentId id,
        double price,
        string currency,
        int quantity,
        string category,
        DateTime dateTimeStart,
        DateTime? dateTimeEnd)
    {
        Id = id;
        Price = price;
        Currency = currency;
        Quantity = quantity;
        Category = category;
        DateTimeStart = dateTimeStart;
        DateTimeEnd = dateTimeEnd;
    }

    public static Investment Create(
         Fund fund,
         int quantity,
         string category,
         DateTime dateTimeStart,
         DateTime? dateTimeEnd)
    {
        return new(
            InvestmentId.CreateUnique(),
            fund.Amount,
            fund.Currency,
            quantity,
            category,
            dateTimeStart,
            dateTimeEnd
        );
    }
}
