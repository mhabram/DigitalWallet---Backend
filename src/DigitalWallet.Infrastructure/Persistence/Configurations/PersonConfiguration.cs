//using DigitalWallet.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DigitalWallet.Infrastructure.Persistence.Configurations;

//public class PersonConfiguration : IEntityTypeConfiguration<Person>
//{
//    public void Configure(EntityTypeBuilder<Person> builder)
//    {
//        builder.HasKey(x => x.Id);
//        builder.Property(x => x.UserId)
//            .IsRequired();
//        builder.Property(x => x.Email)
//            .IsRequired()
//            .HasMaxLength(50);
//        builder.Property(x => x.FirstName)
//            .HasMaxLength(50);
//        builder.Property(x => x.LastName)
//            .HasMaxLength(50);
//        builder.Property(x => x.UserName)
//            .HasMaxLength(50);
//        builder.Property(x => x.CountryCode)
//            .HasMaxLength(5);
//        builder.Property(x => x.PhoneNumber)
//            .HasMaxLength(15);
//    }
//}