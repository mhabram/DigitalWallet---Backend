using DigitalWallet.Domain.AggregateModels.UserAggregate.ValueObjects;
using DigitalWallet.Infrastructure.Identity.Models;

namespace DigitalWallet.Infrastructure.Identity;

public class ApplicationUser : User<Guid, UserId>
{
}