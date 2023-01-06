using System.Reflection;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;
using DigitalWallet.Domain.AggregateModels.WalletAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.Entities;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Infrastructure.Common;
using DigitalWallet.Infrastructure.Common.Constants;
using DigitalWallet.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Investment> Investments => base.Set<Investment>();
    public DbSet<Wallet> Wallets => base.Set<Wallet>();
    public DbSet<WalletHistory> WalletHistories => Set<WalletHistory>();
    public DbSet<InvestmentWalletHistory> InvestmentWalletHistories => Set<InvestmentWalletHistory>();
    public DbSet<InvestmentWallet> InvestmentWallets => Set<InvestmentWallet>();

    public DbSet<Person> Persons => Set<Person>(); // it's going to be deleted in the future

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schemas.Business);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}