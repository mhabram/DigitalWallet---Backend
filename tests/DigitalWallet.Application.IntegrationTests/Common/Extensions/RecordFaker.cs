using System.Runtime.Serialization;
using Bogus;

namespace DigitalWallet.Application.IntegrationTests.Common.Extensions;

public class RecordFaker<T> : Faker<T> where T : class
{
    public RecordFaker()
    {
        CustomInstantiator(_ => (T)FormatterServices.GetUninitializedObject(typeof(T)));
    }
}