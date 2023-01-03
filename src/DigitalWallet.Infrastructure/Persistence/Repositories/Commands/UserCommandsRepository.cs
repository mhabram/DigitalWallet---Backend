using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;
using DigitalWallet.Application.Common.Models;
using DigitalWallet.Domain.Common;
using DigitalWallet.Infrastructure.Identity;
using DigitalWallet.Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Infrastructure.Persistence.Repositories.Commands;

public class UserCommandsRepository : IUserCommandsRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCommandsRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Creates the specified user in the backing store with given
    /// login, email and password, as an asynchronous operation
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="cancellationToken"></param>
    public async Task<Result> CreateUserAsync(IUser user, string password, CancellationToken cancellationToken)
    {
        var applicationUser = new ApplicationUser
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };

        var result = await _userManager.CreateAsync(applicationUser, password);

        return result.ToApplicationResult();
    }
}