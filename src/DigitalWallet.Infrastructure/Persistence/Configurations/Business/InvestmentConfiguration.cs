using DigitalWallet.Domain.AggregateModels.InvestmentAggregate;
using DigitalWallet.Domain.AggregateModels.InvestmentAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.InvestmentWalletAggregate;
using DigitalWallet.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Business;

public sealed class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable(TableNames.Investment);

        builder.Property(i => i.Id)
            .HasConversion(new InvestmentIdConverter());

        builder.Property(i => i.Price)
            .HasPrecision(2)
            .IsRequired();

        builder.Property(i => i.Currency)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.Quantity)
            .IsRequired();

        builder.Property(i => i.Category)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(i => i.DateTimeStart)
            .IsRequired();

        builder.Property(i => i.DateTimeEnd)
            .HasDefaultValue(null);

        builder.Property(i => i.Created)
            .IsRequired();

        builder.Property(i => i.Modified)
            .HasDefaultValue(null);

        builder.HasOne<InvestmentWallet>()
            .WithMany()
            .HasForeignKey(i => i.InvestmentWalletId);
    }
}

public class InvestmentIdConverter : ValueConverter<InvestmentId, Guid>
{
    public InvestmentIdConverter()
        : base(
            id => id.Value,
            value => InvestmentId.CreateUnique())
    { }
}
