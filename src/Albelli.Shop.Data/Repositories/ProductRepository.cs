using Albelli.Shop.Model.Entities;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace Albelli.Shop.Data.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
}

public class ProductRepository : IProductRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public ProductRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public async Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
    {
        var scanConditions = new List<ScanCondition>
        {
            new(
                nameof(EffectiveStatusCode),
                ScanOperator.Equal,
                EffectiveStatusCode.Active),
            new(
                "ProductId",
                ScanOperator.Equal,
                productId)
        };
        var result = await _dynamoDbContext.ScanAsync<Product>(scanConditions,
            new DynamoDBOperationConfig { OverrideTableName = DataOptions.ProductTable }).GetRemainingAsync(cancellationToken);
        return result.Any() ? result.FirstOrDefault() : null;
    }
}