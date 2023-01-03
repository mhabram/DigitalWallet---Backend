using System.Threading;
using DigitalWallet.Application.Common.Exceptions.Common;
using DigitalWallet.Application.Common.Interfaces.Persistence.Commands;
using DigitalWallet.Domain.Entities;
using Moq;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Repositories.Persons;

internal static class MockPersonCommandsRepository
{
    internal static IPersonCommandsRepository GetPersonCommandsRepository(bool withException = false)
    {
        var mockPersonRepository = new Mock<IPersonCommandsRepository>();

        if (!withException)
            SuccessRepository(mockPersonRepository);
        else
            RepositoryWithExceptions(mockPersonRepository);

        return mockPersonRepository.Object;
    }

    #region Repository setup
    private static void SuccessRepository(Mock<IPersonCommandsRepository> mockPersonRepository)
    {
        mockPersonRepository
                    .Setup(s =>
                        s.CreatePersonAsync(
                            It.IsAny<Person>(),
                            It.IsAny<CancellationToken>()));
    }

    private static void RepositoryWithExceptions(Mock<IPersonCommandsRepository> mockPersonRepository)
    {
        mockPersonRepository
                    .Setup(s =>
                        s.CreatePersonAsync(
                            It.IsAny<Person>(),
                            It.IsAny<CancellationToken>()))
                            .ThrowsAsync(new ObjectExistsException(nameof(Person)));
    }
    #endregion

}