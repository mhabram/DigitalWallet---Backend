using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Common.Interfaces.Persistence.Queries;

public interface IPersonQueriesRepository
{
    Task<Person?> GetPersonByEmailAsync(string email, CancellationToken cancellationToken = default);
}