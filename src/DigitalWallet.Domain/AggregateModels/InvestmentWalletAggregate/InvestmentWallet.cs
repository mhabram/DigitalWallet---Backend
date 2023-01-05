using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;

public sealed class InvestmentWallet : IAuditableEntity
{
    private readonly List<InvestmentId> _investmentIds = new();
    private readonly List<InvestmentWalletHistory> _investmentWalletHistories = new();

    public InvestmentWalletId Id { get; private set; }
    public string Name { get; private set; }
    public double Balance { get; private set; }
    public string Currency { get; private set; }
    public WalletId WalletId { get; private set; }

    public IReadOnlyList<InvestmentId> InvestmentIds => _investmentIds.AsReadOnly();
    public IReadOnlyList<InvestmentWalletHistory> InvestmentWalletHistories => _investmentWalletHistories.AsReadOnly();

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private InvestmentWallet(
        InvestmentWalletId id,
        string name,
        double balance,
        string currency,
        WalletId walletId)
    {
        Id = id;
        Name = name;
        Balance = balance;
        Currency = currency;
        WalletId = walletId;
    }

    public static InvestmentWallet Create(
        string name,
        double balance,
        string currency,
        WalletId walletId)
    {
        return new(
            InvestmentWalletId.CreateUnique(),
            name,
            balance,
            currency,
            walletId);
    }

    public void AddInvestment(Investment investment)
    {
        _investmentIds.Add(investment.Id);
    }

    public void RemoveInvestment(Investment investment)
    {
        _investmentIds.Remove(investment.Id);
    }

    public void AddInvestmentHistory(InvestmentWalletHistory investmentHistory)
    {
        _investmentWalletHistories.Add(investmentHistory);
    }
}
