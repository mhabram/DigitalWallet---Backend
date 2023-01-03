using DigitalWallet.Application.Common.Interfaces;

namespace DigitalWallet.Infrastructure.Common.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}