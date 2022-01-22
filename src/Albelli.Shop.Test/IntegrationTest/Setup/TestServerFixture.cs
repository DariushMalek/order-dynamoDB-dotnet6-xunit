using System.IO;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Albelli.Shop.Test.IntegrationTest.Setup;

public class TestServerFixture : WebApplicationFactory<Program>
{
    private const string DynamoDbServiceUrl = "http://localhost:9000/";

    protected override IHostBuilder CreateHostBuilder()
    {
        var hostBuilder = base.CreateHostBuilder()
            .UseEnvironment("Testing")
            .ConfigureAppConfiguration(builder =>
            {
                var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                builder.AddJsonFile(configPath);
            });

        return hostBuilder;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(collection =>
        {
            collection.Remove(new ServiceDescriptor(typeof(IDynamoDBContext),
                a => a.GetService(typeof(IDynamoDBContext)), ServiceLifetime.Scoped));

            var clientConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.EUCentral1,
                UseHttp = true,
                ServiceURL = DynamoDbServiceUrl
            };

            var dynamoDbClient = new AmazonDynamoDBClient(clientConfig);

            collection.AddScoped<IDynamoDBContext, DynamoDBContext>(opt =>
            {
                var client = dynamoDbClient;
                var dynamoDbContext = new DynamoDBContext(client);
                return dynamoDbContext;
            });

            collection.Remove(new ServiceDescriptor(typeof(IAmazonDynamoDB),
                a => a.GetService(typeof(IAmazonDynamoDB)), ServiceLifetime.Scoped));

            collection.AddAWSService<IAmazonDynamoDB>();


            collection.RemoveAll(typeof(IHostedService));
        });
        base.ConfigureWebHost(builder);
    }
}