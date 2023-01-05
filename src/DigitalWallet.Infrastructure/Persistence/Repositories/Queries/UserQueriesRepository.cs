using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Application.Common.Models;
using DigitalWallet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Infrastructure.Persistence.Repositories.Queries;

public class UserQueriesRepository : IUserQueriesRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserQueriesRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Checks if the email is unique
    /// </summary>
    /// <param name="email"></param>
    /// <returns>
    /// The System.Threading.Tasks.Task that represents the asynchronous operation,
    /// containing bool.
    /// </returns>
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user == null;
    }

    public async Task<BaseUser> SignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new SignInCommandException();

        var isValid = await _userManager.CheckPasswordAsync(user, password);

        if (!isValid)
            throw new SignInCommandException();

        return new BaseUser(
            Guid.NewGuid(),
            user.Email,
            user.UserName);
    }
}