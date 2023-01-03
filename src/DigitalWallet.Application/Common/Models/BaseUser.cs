using DigitalWallet.Domain.Common;

namespace DigitalWallet.Application.Common.Models;

public class BaseUser : IUser
{
    public BaseUser()
    {

    }

    public BaseUser(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public BaseUser(Guid id, string userName, string email, string? phoneNumber)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = string.Empty;
}