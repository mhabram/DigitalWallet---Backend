using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Infrastructure.Identity.Models;

public abstract class User<TKey, TValueObject> : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
    where TValueObject : notnull
{
    public new TValueObject? Id { get; set; }
}
