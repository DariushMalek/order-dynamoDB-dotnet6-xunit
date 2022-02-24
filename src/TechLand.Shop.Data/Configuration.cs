using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using TechLand.Shop.Data.Repositories;

namespace TechLand.Shop.Data;

public static class Configuration
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services.AddTransient<IProductTypeRepository, ProductTypeRepository>()
            .AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IOrderRepository, OrderRepository>()
            .AddSingleton<IDynamoDBContext>(c => new DynamoDBContext(c.GetService<IAmazonDynamoDB>()));
    }
}