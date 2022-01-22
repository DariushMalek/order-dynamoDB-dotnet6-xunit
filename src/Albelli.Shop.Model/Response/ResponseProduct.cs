namespace Albelli.Shop.Model.Response;

public class ResponseProduct
{
    public int ProductTypeId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public double WidthInMillimeters { get; set; }

    public int StackSize { get; set; }
}