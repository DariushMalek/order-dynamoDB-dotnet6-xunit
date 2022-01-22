using Xunit;

namespace Albelli.Shop.Test.IntegrationTest.Setup;

[CollectionDefinition("api")]
public class CollectionFixture : 
    ICollectionFixture<TestServerFixture>,
    ICollectionFixture<TestDataSetup>
{
}