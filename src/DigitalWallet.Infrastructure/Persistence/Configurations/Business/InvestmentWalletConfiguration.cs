using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Business;

public sealed class InvestmentWalletConfiguration : IEntityTypeConfiguration<InvestmentWallet>
{
    public void Configure(EntityTypeBuilder<InvestmentWallet> builder)
    {
        builder.ToTable(TableNames.InvestmentWallet);


        builder.Property(i => i.Id)
            .HasConversion(new InvestmentWalletIdConverter());

        builder.Property(i => i.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.Balance)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(i => i.Currency)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.WalletId)
            .HasConversion(new WalletIdConverter());

        builder.Property(i => i.Created)
            .IsRequired();

        builder.Property(i => i.Modified)
            .HasDefaultValue(null);

        builder.Ignore(i => i.InvestmentIds);

        builder.Ignore(i => i.InvestmentWalletHistories);
    }
}

public class InvestmentWalletIdConverter : ValueConverter<InvestmentWalletId, Guid>
{
    public InvestmentWalletIdConverter()
        : base(
            id => id.Value,
            value => InvestmentWalletId.CreateUnique())
    { }
}
