namespace DigitalWallet.Domain.Common;

public interface IUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}