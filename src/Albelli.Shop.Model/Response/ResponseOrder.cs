namespace Albelli.Shop.Model.Response;

public class ResponseOrder 
{
    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public List<ResponseOrderLine> Lines { get; set; }

    public double RequiredBinWidthInMillimeters { get; set; }
}

public class ResponseOrderLine
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
}