using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.Entities;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Business;

public sealed class WalletHistoryConfiguration : IEntityTypeConfiguration<WalletHistory>
{
    public void Configure(EntityTypeBuilder<WalletHistory> builder)
    {
        builder.ToTable(TableNames.WalletHistory);

        builder.Property(w => w.Id)
            .HasConversion(new WalletHistoryIdConverter());

        builder.Property(w => w.Amount)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(w => w.Currency)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(w => w.Deposit)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(w => w.WalletId)
            .HasConversion(new WalletIdConverter());

        builder.Property(w => w.Created)
            .IsRequired();

        builder.Property(w => w.Modified)
            .HasDefaultValue(null);

        builder.HasOne<Wallet>()
            .WithMany()
            .HasForeignKey(w => w.WalletId);
    }
}

public class WalletHistoryIdConverter : ValueConverter<WalletHistoryId, Guid>
{
    public WalletHistoryIdConverter()
        : base(
            id => id.Value,
            value => WalletHistoryId.CreateUnique())
    { }
}
