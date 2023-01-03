using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.IntegrationTests.Common.TestData;
using DigitalWallet.Domain.Common;
using Moq;

namespace DigitalWallet.Application.IntegrationTests.Common.Mocks.Token;

internal static class MockJwtTokenGeneratorService
{

    internal static IJwtTokenGenerator GetTokenService()
    {
        var mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();

        mockJwtTokenGenerator.Setup(s =>
            s.GenerateToken(
                It.IsAny<IUser>()))
            .Returns(TokenTestData.GeneratedToken);

        return mockJwtTokenGenerator.Object;
    }
}
