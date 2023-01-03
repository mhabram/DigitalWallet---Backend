using DigitalWallet.Application.Common.Models;
using DigitalWallet.Domain.Common;

namespace DigitalWallet.Application.Common.Interfaces.Persistence.Commands;

public interface IUserCommandsRepository
{
    Task<Result> CreateUserAsync(IUser user, string password, CancellationToken cancellationToken = default);
}