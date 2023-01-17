using CompanyWebcast.UnitTests.Fixtures;
using Xunit;

namespace CompanyWebcast.UnitTests.Helpers.Collections
{
    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
