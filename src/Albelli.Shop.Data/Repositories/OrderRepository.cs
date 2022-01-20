using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Data.Repositories
{
    public interface IOrderRepository
    {

    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public OrderRepository(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }
    }
}
