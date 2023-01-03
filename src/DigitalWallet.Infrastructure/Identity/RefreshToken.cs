namespace DigitalWallet.Infrastructure.Identity;

public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; } = null!;
    public string JwtId { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }

    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = new ApplicationUser();
}
