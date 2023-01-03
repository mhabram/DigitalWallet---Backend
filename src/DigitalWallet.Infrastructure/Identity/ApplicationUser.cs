using DigitalWallet.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>, IUser
{
}