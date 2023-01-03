using DigitalWallet.Domain.AggregateModels.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.UserAggregate;

public sealed class User : IAuditableEntity
{
    public UserId UserId { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? CountryCode { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private User(
        UserId userId,
        string email,
        string userName,
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? countryCode)
    {
        UserId = userId;
        Email = email;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        CountryCode = countryCode;
    }

    public static User Create(
        UserId userId,
        string email,
        string userName,
        string? firstName,
        string? lastname,
        string? phoneNumber,
        string? countryCode)
    {
        return new(
            userId,
            email,
            userName,
            firstName,
            lastname,
            phoneNumber,
            countryCode);
    }
}
