using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using TechLand.Shop.Model.Entities;

namespace TechLand.Shop.Data.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);
    Task InsertAsync(Order order, CancellationToken cancellationToken);
}

public class OrderRepository : IOrderRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public OrderRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
    {
        var scanConditions = new List<ScanCondition>
        {
            new(
                nameof(EffectiveStatusCode),
                ScanOperator.Equal,
                EffectiveStatusCode.Active),
            new(
                "OrderId",
                ScanOperator.Equal,
                orderId)
        };
        var result = await _dynamoDbContext.ScanAsync<Order>(scanConditions,
            new DynamoDBOperationConfig { OverrideTableName = DataOptions.OrderTable }).GetRemainingAsync(cancellationToken);

        if (result.Any())
        {
            return result.FirstOrDefault();
        }
        else
        {
            return null;
        }
    }

    public async Task InsertAsync(Order order, CancellationToken cancellationToken)
    {
        await _dynamoDbContext.SaveAsync(
            order,
            new DynamoDBOperationConfig { OverrideTableName = DataOptions.OrderTable, SkipVersionCheck = true },
            cancellationToken);
    }
}