namespace DigitalWallet.Domain.Common.Models;

public interface IAuditableEntity
{
    public DateTime Created { get; }
    public DateTime? Modified { get; }
}
