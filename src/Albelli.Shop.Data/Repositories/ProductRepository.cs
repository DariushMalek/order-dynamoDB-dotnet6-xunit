using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Data.Repositories;

public interface IProductRepository
{

}

public class ProductRepository : IProductRepository
{
    private readonly IDynamoDBContext _dynamoDbContext;

    public ProductRepository(IDynamoDBContext dynamoDbContext)
    {
        _dynamoDbContext = dynamoDbContext;
    }
}