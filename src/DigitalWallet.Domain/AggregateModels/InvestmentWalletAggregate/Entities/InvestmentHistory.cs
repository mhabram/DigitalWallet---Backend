using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.ValueObjects;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;

public sealed class InvestmentHistory
{
    public InvestmentHistoryId InvestmentHistoryId { get; private set; }
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    public bool Purchase { get; private set; }
    public WalletId WalletId { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private InvestmentHistory(
        InvestmentHistoryId investmentHistoryId,
        double amount,
        string currency,
        bool purchase,
        WalletId walletId)
    {
        InvestmentHistoryId = investmentHistoryId;
        Amount = amount;
        Currency = currency;
        Purchase = purchase;
        WalletId = walletId;
    }

    public static InvestmentHistory Create(
        Fund fund,
        bool purchase,
        WalletId walletId)
    {
        return new(
            InvestmentHistoryId.CreateUnique(),
            fund.Amount,
            fund.Currency,
            purchase,
            walletId
        );
    }
}
