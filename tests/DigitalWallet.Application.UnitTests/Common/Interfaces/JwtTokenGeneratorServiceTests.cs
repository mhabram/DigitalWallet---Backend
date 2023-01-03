using DigitalWallet.Application.Common.Interfaces;
using DigitalWallet.Application.UnitTests.Common.Mocks.Fakers.Authentication;
using DigitalWallet.Domain.Common;
using DigitalWallet.Domain.Entities;
using DigitalWallet.Infrastructure.Common.Authentication;
using DigitalWallet.Infrastructure.Common.Services;
using DigitalWallet.Infrastructure.Identity.Settings;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Xunit;

namespace DigitalWallet.Application.UnitTests.Common.Interfaces;

public class JwtTokenGeneratorServiceTests
{
    private readonly IJwtTokenGenerator _jwtTokenGeneratorService;

    public JwtTokenGeneratorServiceTests()
    {
        var dateTimeService = new DateTimeProvider();

        var jwtSettings = Options.Create(new JwtSettings()
        {
            ExpiryMinutes = 60,
            Secret = "KQWge7v1EVjeQQe9oHi5lor73hD975vb",
            Issuer = "DigitalWallet",
            Audience = "DigitalWallet"
        });

        _jwtTokenGeneratorService = new JwtTokenGenerator(
            dateTimeService,
            jwtSettings);
    }

    [Fact]
    public void GenerateTokenReturnValidToken()
    {
        var user = new BaseUserFaker().Generate();
        string token = _jwtTokenGeneratorService.GenerateToken(user);

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);

        string tokenSub = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        string tokenUniqueName = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName).Value;
        string tokenEmail = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
        Guid tokenJti = Guid.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Jti).Value);

        tokenJti.Should().NotBeEmpty();
        tokenSub.Should().Be(user.Id.ToString());
        tokenUniqueName.Should().Be(user.UserName);
        tokenEmail.Should().Be(user.Email);
    }
}
