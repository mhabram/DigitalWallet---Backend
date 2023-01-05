using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.Entities;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Business;

public sealed class InvestmentHistoryConfiguration : IEntityTypeConfiguration<InvestmentWalletHistory>
{
    public void Configure(EntityTypeBuilder<InvestmentWalletHistory> builder)
    {
        builder.ToTable(TableNames.InvestmentWalletHistory);

        builder.Property(i => i.Id)
            .HasConversion(new InvestmentWalletHistoryIdConverter());

        builder.Property(i => i.Amount)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(i => i.Currency)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Purchase)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(i => i.InvestmentWalletId)
            .HasConversion(new InvestmentWalletIdConverter());

        builder.Property(i => i.Created)
            .IsRequired();

        builder.Property(i => i.Modified)
            .HasDefaultValue(null);

        builder.HasOne<InvestmentWallet>()
            .WithMany()
            .HasForeignKey(i => i.InvestmentWalletId);
    }
}

public class InvestmentWalletHistoryIdConverter : ValueConverter<InvestmentWalletHistoryId, Guid>
{
    public InvestmentWalletHistoryIdConverter()
        : base(
            id => id.Value,
            value => InvestmentWalletHistoryId.CreateUnique())
    { }
}