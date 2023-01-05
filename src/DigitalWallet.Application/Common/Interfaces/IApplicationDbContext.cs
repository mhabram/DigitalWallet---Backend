using Microsoft.EntityFrameworkCore;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;
using DigitalWallet.Domain.AggregateModels.WalletAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.Entities;

namespace DigitalWallet.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Investment> Investments { get; }
    DbSet<Wallet> Wallets { get; }
    DbSet<WalletHistory> WalletHistories { get; }
    DbSet<InvestmentWalletHistory> InvestmentWalletHistories { get; }
    DbSet<InvestmentWallet> InvestmentWallets { get; }
    DbSet<Person> Persons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
