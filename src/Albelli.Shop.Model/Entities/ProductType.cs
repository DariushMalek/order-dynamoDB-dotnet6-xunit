using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Model.Entities
{
    public class ProductType : Entity
    {
        [DynamoDBHashKey]
        public int ProductTypeId { get; set;}

        [DynamoDBProperty]
        public string ProductTypeName { get; set; }
    }
}