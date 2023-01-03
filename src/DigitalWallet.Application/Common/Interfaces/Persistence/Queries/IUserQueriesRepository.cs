using DigitalWallet.Application.Common.Models;

namespace DigitalWallet.Application.Common.Interfaces.Persistence.Queries;

public interface IUserQueriesRepository
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<BaseUser> SignInAsync(string email, string password, CancellationToken cancellationToken);
}