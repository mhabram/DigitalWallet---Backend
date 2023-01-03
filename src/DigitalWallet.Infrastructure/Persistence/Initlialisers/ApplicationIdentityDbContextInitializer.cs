using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Infrastructure.Persistence.Initlialisers;

public class ApplicationIdentityDbContextInitialiser
{
	private readonly ILogger<ApplicationIdentityDbContextInitialiser> _logger;
	private readonly ApplicationIdentityDbContext _context;

	public ApplicationIdentityDbContextInitialiser(
		ILogger<ApplicationIdentityDbContextInitialiser> logger,
		ApplicationIdentityDbContext context)
	{
		_logger = logger;
		_context = context;
	}

	public async Task InitialiseAsync()
	{
		try
		{
			if (_context.Database.IsNpgsql())
				await _context.Database.MigrateAsync();
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database.");
			throw;
		}
	}

	//public async Task SeedAsync()
	//{
	//	try
	//	{
	//		await TrySeedAsync();
	//	}
	//	catch (Exception ex)
	//	{
	//		_logger.LogError(ex, "An error occurred while seeding the database.");
	//		throw;
	//	}
	//}
}
