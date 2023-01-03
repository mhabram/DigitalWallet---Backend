using System.Threading;
using DigitalWallet.Application.Common.Interfaces.Persistence.Queries;
using Moq;

namespace DigitalWallet.Application.UnitTests.Mocks.Users;

internal static class MockUserQueriesRepository
{
    internal static IUserQueriesRepository GetQueriesRepository(bool isUniqueEmail = true)
    {
        var mockUserQueriesRepository = new Mock<IUserQueriesRepository>();

        mockUserQueriesRepository
            .Setup(s =>
                s.IsEmailUniqueAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(isUniqueEmail);

        return mockUserQueriesRepository.Object;
    }
}