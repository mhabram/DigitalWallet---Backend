using DigitalWallet.Application.Common.Exceptions.Authentication;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using DigitalWallet.Application.Common.Models;
using Moq;
using System.Threading;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Users;

internal class MockUserQueriesRepository
{
    internal static IUserQueriesRepository GetUserQueriesRepository(BaseUser? user = null, bool withException = false)
    {
        var mockUserRepository = new Mock<IUserQueriesRepository>();

        if (!withException)
            SuccessRepository(mockUserRepository, user);
        else
            RepositoryWithExceptions(mockUserRepository);

        return mockUserRepository.Object;
    }

    private static void RepositoryWithExceptions(Mock<IUserQueriesRepository> mockUserRepository)
    {
        mockUserRepository
            .Setup(s =>
                s.SignInAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ThrowsAsync(new SignInCommandException());
    }

    private static void SuccessRepository(Mock<IUserQueriesRepository> mockUserRepository, BaseUser? user)
    {
        if (user is not null)
        {
            mockUserRepository
                .Setup(s =>
                    s.SignInAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
        }
    }
}
