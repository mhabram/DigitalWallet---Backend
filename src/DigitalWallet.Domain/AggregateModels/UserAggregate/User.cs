using DigitalWallet.Domain.AggregateModels.UserAggregate.ValueObjects;
using DigitalWallet.Domain.AggregateModels.WalletAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Models;

namespace DigitalWallet.Domain.AggregateModels.UserAggregate;

public sealed class User : IAuditableEntity
{
    private readonly List<WalletId> _walletIds = new();

    public UserId Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? CountryCode { get; private set; }

    public IReadOnlyList<WalletId>? WalletIds => _walletIds.AsReadOnly();

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    private User(
        UserId id,
        string email,
        string userName,
        string? firstName,
        string? lastName,
        string? phoneNumber,
        string? countryCode)
    {
        Id = id;
        Email = email;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        CountryCode = countryCode;
    }

    public static User Create(
        UserId id,
        string email,
        string userName,
        string? firstName,
        string? lastname,
        string? phoneNumber,
        string? countryCode)
    {
        return new(
            id,
            email,
            userName,
            firstName,
            lastname,
            phoneNumber,
            countryCode);
    }
}
