using System.Threading;
using DigitalWallet.Application.Common.Models;
using Moq;
using System;
using DigitalWallet.Domain.Common;
using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Users;

internal static class MockUserCommandsRepository
{
    internal static IUserCommandsRepository GetUserCommandsRepository(bool withException = false)
    {
        var mockUserRepository = new Mock<IUserCommandsRepository>();

        if (!withException)
            SuccessRepository(mockUserRepository);
        else
            RepositoryWithExceptions(mockUserRepository);

        return mockUserRepository.Object;
    }

    #region Repository setup
    private static void SuccessRepository(Mock<IUserCommandsRepository> mockUserRepository)
    {
        mockUserRepository
            .Setup(s =>
                s.CreateUserAsync(
                    It.IsAny<IUser>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetCreatedUserResponse());
    }

    private static void RepositoryWithExceptions(Mock<IUserCommandsRepository> mockUserRepository)
    {
        mockUserRepository
                    .Setup(s =>
                        s.CreateUserAsync(
                            It.IsAny<IUser>(),
                            It.IsAny<string>(),
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(GetCreatedUserResponseFalse());
    }
    #endregion

    #region TestData
    private static Result GetCreatedUserResponse()
    {
        return new Result
        {
            Succeeded = true,
            Errors = Array.Empty<string>()
        };
    }

    private static Result GetCreatedUserResponseFalse()
    {
        return new Result
        {
            Succeeded = false,
            Errors = new string[] { "User could not be saved" }
        };
    }
    #endregion
}
