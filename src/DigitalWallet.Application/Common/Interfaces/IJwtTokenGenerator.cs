using DigitalWallet.Domain.Common;

namespace DigitalWallet.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(IUser user);
}