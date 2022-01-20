using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Data.Repositories
{
    public interface IProductTypeRepository
    {

    }

    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public ProductTypeRepository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }
    }
}
