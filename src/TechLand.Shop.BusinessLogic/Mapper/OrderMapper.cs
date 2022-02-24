using TechLand.Shop.Model.Entities;
using TechLand.Shop.Model.Requests;
using TechLand.Shop.Model.Response;

namespace TechLand.Shop.BusinessLogic.Mapper;

public interface IOrderMapper
{
    IEnumerable<Order> Convert(IEnumerable<RequestOrder> orderResponseModel);
    Order Convert(RequestOrder orderRequestModel);
    IEnumerable<ResponseOrder> Convert(IEnumerable<Order> orderResponseModel);
    ResponseOrder Convert(Order orderModel);
}

public  class OrderMapper : IOrderMapper
{
    public IEnumerable<Order> Convert(IEnumerable<RequestOrder> orderResponseModel)
    {
        return orderResponseModel.Select(Convert);
    }

    public Order Convert(RequestOrder orderRequestModel)
    {
        var orderModel = new Order
        {
            OrderId = orderRequestModel.OrderId,
            CustomerId = orderRequestModel.CustomerId,
            Lines = orderRequestModel.Lines.Select(n => new OrderLine()
                {
                    ProductId = n.ProductId,
                    Quantity = n.Quantity
                }
            ).ToList()
        };
        return orderModel;
    }

    public IEnumerable<ResponseOrder> Convert(IEnumerable<Order> orderResponseModel)
    {
        return orderResponseModel.Select(Convert);
    }

    public ResponseOrder Convert(Order orderModel)
    {
        var orderResponseModel = new ResponseOrder
        {
            OrderId = orderModel.OrderId,
            CustomerId = orderModel.CustomerId,
            RequiredBinWidthInMillimeters = orderModel.RequiredBinWidthInMillimeters,
            Lines = orderModel.Lines.Select(n => new ResponseOrderLine()
                {
                    ProductId = n.ProductId,
                    Quantity = n.Quantity
                }
            ).ToList()
        };
        return orderResponseModel;
    }
}