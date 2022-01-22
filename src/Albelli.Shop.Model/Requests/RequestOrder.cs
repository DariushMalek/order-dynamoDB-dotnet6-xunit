namespace Albelli.Shop.Model.Requests;

public class RequestOrder
{
    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public List<RequestOrderLine> Lines { get; set; }

    public double RequiredBinWidthInMillimeters { get; set; }
}

public class RequestOrderLine
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
}