using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Exceptions.Common;
using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Domain.Entities;

namespace DigitalWallet.Infrastructure.Persistence.Repositories.Commands;

public class PersonCommandsRepository : IPersonCommandsRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IPersonQueriesRepository _personQueriesRepository;

    public PersonCommandsRepository(
        IApplicationDbContext context,
        IPersonQueriesRepository personQueriesRepository)
    {
        _context = context;
        _personQueriesRepository = personQueriesRepository;
    }

    public async Task CreatePersonAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            if (await _personQueriesRepository
                .GetPersonByEmailAsync(person.Email, cancellationToken) != null)
            {
                throw new ObjectExistsException(nameof(person));
            }

            await _context.Persons
                .AddAsync(person, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw new SignUpCommandException();
        }
    }
}