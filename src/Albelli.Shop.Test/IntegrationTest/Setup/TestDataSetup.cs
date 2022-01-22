using Amazon.DynamoDBv2;

namespace Albelli.Shop.Test.IntegrationTest.Setup;

public class TestDataSetup
{
    private static readonly IAmazonDynamoDB DynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
    {
        ServiceURL = "http://localhost:9000"
    });

    public TestDataSetup()
    {

    }
}