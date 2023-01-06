using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.UserAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;
using DigitalWallet.Domain.Common.ValueObjects;

namespace DigitalWallet.Domain.AggregateModels.WalletAggregate;

public sealed class Wallet : IAuditableEntity
{
    private static readonly double _amount = 0.0;

    private readonly List<WalletHistoryId> _walletHistoryIds = new();
    private readonly List<InvestmentWalletId> _investmentWalletIds = new();

    public WalletId Id { get; private set; }
    public double Balance { get; private set; }
    public string Currency { get; private set; }
    public UserId UserId { get; private set; }

    public IReadOnlyCollection<WalletHistoryId> WalletHistoryIds => _walletHistoryIds.AsReadOnly();
    public IReadOnlyCollection<InvestmentWalletId> InvestmentWalletIds => _investmentWalletIds.AsReadOnly();

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private Wallet(
        WalletId id,
        double balance,
        string currency,
        UserId userId)
    {
        Id = id;
        Balance = balance;
        Currency = currency;
        UserId = userId;
    }

    public static Wallet Create(
        string currencyCode,
        UserId userId)
    {
        return new(
            WalletId.CreateUnique(),
            _amount,
            currencyCode,
            userId);
    }

    public void DepositFunds(Fund fund)
    {
        if (Currency == fund.Currency)
        {
            Balance += fund.Amount;
        }
    }

    public void WithdrawFunds(Fund fund)
    {
        if (Currency == fund.Currency)
        {
            Balance -= fund.Amount;
        }
    }

    public void AddInvestmentWallet(InvestmentWallet investmentWallet)
    {
        _investmentWalletIds.Add(investmentWallet.Id);
    }

    public void RemoveInvestmentWallet(InvestmentWallet investmentWallet)
    {
        _investmentWalletIds.Remove(investmentWallet.Id);
    }
}
