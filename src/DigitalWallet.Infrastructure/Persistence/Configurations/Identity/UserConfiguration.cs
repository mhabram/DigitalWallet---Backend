using DigitalWallet.Domain.AggregateModels.UserAggregate;
using DigitalWallet.Domain.AggregateModels.UserAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalWallet.Infrastructure.Persistence.Configurations.Identity;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.User, Schemas.Identity);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(new UserIdConverter());

        builder.Property(u => u.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.UserName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(u => u.CountryCode)
            .HasMaxLength(10);

        builder.Property(u => u.Created)
            .IsRequired();

        builder.Property(u => u.Modified)
            .HasDefaultValue(null);

        builder.Ignore(u => u.WalletIds);
    }
}

public class UserIdConverter : ValueConverter<UserId, Guid>
{
    public UserIdConverter()
        : base(
            id => id.Value,
            value => UserId.CreateUnique())
    { }
}
