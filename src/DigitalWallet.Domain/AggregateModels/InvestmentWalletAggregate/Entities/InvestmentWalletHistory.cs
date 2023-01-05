using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.ValueObjects;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;

public sealed class InvestmentWalletHistory
{
    public InvestmentWalletHistoryId Id { get; private set; }
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    public bool Purchase { get; private set; }
    public InvestmentWalletId InvestmentWalletId { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private InvestmentWalletHistory(
        InvestmentWalletHistoryId id,
        double amount,
        string currency,
        bool purchase,
        InvestmentWalletId investmentWalletId)
    {
        Id = id;
        Amount = amount;
        Currency = currency;
        Purchase = purchase;
        InvestmentWalletId = investmentWalletId;
    }

    public static InvestmentWalletHistory Create(
        Fund fund,
        bool purchase,
        InvestmentWalletId investmentWalletId)
    {
        return new(
            InvestmentWalletHistoryId.CreateUnique(),
            fund.Amount,
            fund.Currency,
            purchase,
            investmentWalletId
        );
    }
}
