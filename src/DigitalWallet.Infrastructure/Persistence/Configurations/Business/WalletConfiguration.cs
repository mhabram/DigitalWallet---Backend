using DigitalWallet.Domain.AggregateModels.UserAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Common.Constants;
using DigitalWallet.Infrastructure.Persistence.Configurations.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Business;

public sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(TableNames.Wallet);

        builder.Property(w => w.Id)
            .HasConversion(new WalletIdConverter());

        builder.Property(w => w.Balance)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(w => w.Currency)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(w => w.UserId)
            .HasConversion(new UserIdConverter());

        builder.Property(w => w.Created)
            .IsRequired();

        builder.Property(w => w.Modified)
            .HasDefaultValue(null);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(u => u.UserId);

        builder.Ignore(w => w.WalletHistoryIds);

        builder.Ignore(w => w.InvestmentWalletIds);
    }
}

public class WalletIdConverter : ValueConverter<WalletId, Guid>
{
    public WalletIdConverter()
        : base(
            id => id.Value,
            value => WalletId.CreateUnique())
    { }
}
