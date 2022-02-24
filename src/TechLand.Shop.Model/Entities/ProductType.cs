using Amazon.DynamoDBv2.DataModel;

namespace TechLand.Shop.Model.Entities;

public class ProductType : Entity
{
    [DynamoDBHashKey]
    public int ProductTypeId { get; set;}

    [DynamoDBProperty]
    public string ProductTypeName { get; set; }
}