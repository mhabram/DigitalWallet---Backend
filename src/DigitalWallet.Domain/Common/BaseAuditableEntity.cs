namespace DigitalWallet.Domain.Common;

/// <summary>
/// Provides basic security information in case that
/// something may fail in database or entity
/// </summary>
public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}