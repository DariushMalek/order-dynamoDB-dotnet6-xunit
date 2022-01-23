namespace Albelli.Shop.Model;

public static class Messages
{
    public const string OrderIsExist = "Order is Exists!";
    public const string OrderNotFound = "Order not found!";
    public const string OrderHasNotAnyProduct = "Order has not any product!";
    public const string ProductIsDuplicated = "Product is duplicated!";

    public static object ProductNotFound { get; set; }
}