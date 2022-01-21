using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Model.Entities;

public class Order : Entity
{
    [DynamoDBHashKey]
    public int CustomerId { get; set; }

    [DynamoDBRangeKey]
    public int OrderId { get; set; }

    public List<OrderLine> Lines { get; set; }

    [DynamoDBProperty]
    public double RequiredBinWidthInMillimeters { get; set; }
}

public class OrderLine
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
}