using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;
using DigitalWallet.Domain.Common.ValueObjects;

namespace DigitalWallet.Domain.AggregateModels.WalletAggregate.Entities;

public sealed class WalletHistory : IAuditableEntity
{
    public WalletHistoryId Id { get; private set; }
    public double Amount { get; private set; }
    public string Currency { get; private set; }
    public bool Deposit { get; private set; }
    public WalletId WalletId { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private WalletHistory(
        WalletHistoryId id,
        double amount,
        string currency,
        bool deposit,
        WalletId walletId)
    {
        Id = id;
        Currency = currency;
        Amount = amount;
        Deposit = deposit;
        WalletId = walletId;
    }

    public static WalletHistory Create(
        Fund fund,
        bool deposit,
        WalletId walletId)
    {
        return new(
            WalletHistoryId.CreateUnique(),
            fund.Amount,
            fund.Currency,
            deposit,
            walletId);
    }
}
