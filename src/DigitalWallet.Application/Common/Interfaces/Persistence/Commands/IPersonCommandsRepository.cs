using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Common.Interfaces.Persistence.Commands;

public interface IPersonCommandsRepository
{
    Task CreatePersonAsync(Person person, CancellationToken cancellationToken = default);
}