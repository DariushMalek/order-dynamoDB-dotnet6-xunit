using Albelli.Shop.Model.Entities;
using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Data.Repositories;

public interface IProductTypeRepository
{
    Task<ProductType> GetBrProductTypeIdAsync(int productTypeId, CancellationToken cancellationToken);
}

public class ProductTypeRepository : IProductTypeRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public ProductTypeRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }

    public async Task<ProductType> GetBrProductTypeIdAsync(int productTypeId, CancellationToken cancellationToken)
    {
        return await _dynamoDbContext.LoadAsync<ProductType>(productTypeId,
            new DynamoDBOperationConfig { OverrideTableName = DataOptions.ProductTypeTable },
            cancellationToken);
    }
}