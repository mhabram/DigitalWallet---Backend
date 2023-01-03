using System.Collections.Generic;
using FluentAssertions;
using FluentValidation.Results;

namespace DigitalWallet.Application.UnitTests.Common.Extensions;

public static class TestExtensions
{
    public static void AssertPropertyName(this List<ValidationFailure> errors, string expectedMsg)
    {
        foreach (var error in errors)
            error.PropertyName.Should().BeEquivalentTo(expectedMsg.ToLower());
    }
}