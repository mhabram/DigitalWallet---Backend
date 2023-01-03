using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Persistence.Repositories.Queries;

public class PersonQueriesRepository : IPersonQueriesRepository
{
    private readonly IApplicationDbContext _context;

    public PersonQueriesRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetPersonByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(x =>
                x.Email == email,
                cancellationToken);
    }
}