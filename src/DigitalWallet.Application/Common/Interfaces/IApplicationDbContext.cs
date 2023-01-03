using Microsoft.EntityFrameworkCore;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Person> Persons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
