using Albelli.Shop.Model.Entities;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Albelli.Shop.Data.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    Task InsertAsync(Order order, CancellationToken cancellationToken);
}

public class OrderRepository : IOrderRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public OrderRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public async Task<Order> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken)
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
        return result.FirstOrDefault();
    }

    public async Task InsertAsync(Order order, CancellationToken cancellationToken)
    {
        await _dynamoDbContext.SaveAsync(
            order,
            new DynamoDBOperationConfig { OverrideTableName = DataOptions.OrderTable, SkipVersionCheck = true },
            cancellationToken);
    }
}