using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Model.Entities;

public class Product
{
    [DynamoDBHashKey]
    public int ProductTypeId { get; set; }

    [DynamoDBRangeKey]
    public int ProductId { get; set; }

    [DynamoDBProperty]
    public string ProductName { get; set; }

    //Some products with same type may have different width
    //For example: two calendars may differ in size and shape
    [DynamoDBProperty]
    public double WidthInMillimeters { get; set; }

    [DynamoDBProperty]
    public int StackSize { get; set; }
}