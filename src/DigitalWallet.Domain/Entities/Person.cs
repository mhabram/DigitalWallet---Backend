using DigitalWallet.Domain.Common;

namespace DigitalWallet.Domain.Entities;

public class Person : BaseAuditableEntity, IUser
{
    public Person() { }
    public Person(Guid userId, string firstName, string lastName, string email, string userName, string countryCode, string phoneNumber)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
        CountryCode = countryCode;
        PhoneNumber = phoneNumber;
    }

    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? CountryCode { get; set; } = string.Empty;
}