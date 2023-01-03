﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Infrastructure.Persistence.Initlialisers;

public class ApplicationDbContextInitialiser
{
	private readonly ILogger<ApplicationDbContextInitialiser> _logger;
	private readonly ApplicationDbContext _context;

	public ApplicationDbContextInitialiser(
		ILogger<ApplicationDbContextInitialiser> logger,
		ApplicationDbContext context)
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
		catch(Exception ex)
		{
			_logger.LogError(ex, "An error occurred while initialising the database.");
			throw;
		}
	}
}
