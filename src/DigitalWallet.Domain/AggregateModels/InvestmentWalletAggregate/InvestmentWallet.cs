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
    private readonly List<InvestmentHistory> _investmentHistories = new();

    public InvestmentWalletId InvestmentWalletId { get; private set; }
    public string Name { get; private set; }
    public double Balance { get; private set; }
    public string Currency { get; private set; }
    public WalletId WalletId { get; private set; }

    public IReadOnlyList<InvestmentId> InvestmentIds => _investmentIds.AsReadOnly();
    public IReadOnlyList<InvestmentHistory> InvestmentHistories => _investmentHistories.AsReadOnly();

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private InvestmentWallet(
        InvestmentWalletId investmentWalletId,
        string name,
        double balance,
        string currency,
        WalletId walletId)
    {
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
        _investmentIds.Add(investment.InvestmentId);
    }

    public void RemoveInvestment(Investment investment)
    {
        _investmentIds.Remove(investment.InvestmentId);
    }

    public void AddInvestmentHistory(InvestmentHistory investmentHistory)
    {
        _investmentHistories.Add(investmentHistory);
    }
}
