using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model;
using Albelli.Shop.Model.Entities;
using Albelli.Shop.Model.Requests;
using Albelli.Shop.Model.Response;

namespace Albelli.Shop.BusinessLogic;

public interface IOrderService
{
    public Task<ResponseResult> CreateOrder(RequestOrder order, CancellationToken cancellationToken);

    public Task<ResponseResult> GetOrder(int orderId, CancellationToken cancellationToken);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IProductService _productService;

    private readonly IOrderMapper _orderMapper;

    public OrderService(IOrderRepository orderRepository, IOrderMapper orderMapper, IProductService productService)
    {
        _orderRepository = orderRepository;
        _orderMapper = orderMapper;
        _productService = productService;
    }

    public async Task<ResponseResult> CreateOrder(RequestOrder requestOrder, CancellationToken cancellationToken)
    {
        if (await OrderIsExist(requestOrder.OrderId, cancellationToken))
        {
            return new ResponseResult(Messages.OrderIsExist);
        }

        if (!requestOrder.Lines.Any())
        {
            return new ResponseResult(Messages.OrderHasNotAnyProduct);
        }

        if (requestOrder.Lines.GroupBy(n=>n.ProductId).Any(a=>a.Count() > 1))
        {
            return new ResponseResult(Messages.ProductIsDuplicated);
        }

        var order = _orderMapper.Convert(requestOrder);
        order.CreatedAt = DateTime.Now;
        order.StatusCode = EffectiveStatusCode.Active;
        order.RequiredBinWidthInMillimeters = await CalcRequiredBinWidth(order.Lines, cancellationToken);
        await _orderRepository.InsertAsync(order, cancellationToken);

        return new ResponseResult(_orderMapper.Convert(order));
    }

    public async Task<ResponseResult> GetOrder(int orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId, cancellationToken);
        if (order == null)
        {
            return new ResponseResult(Messages.OrderNotFound);
        }
        return new ResponseResult(_orderMapper.Convert(order));
    }

    private async Task<double> CalcRequiredBinWidth(List<OrderLine> orderLines, CancellationToken cancellationToken)
    {
        double totalWidth = 0;
        foreach (var line in orderLines)
        {
            var productInfo = await _productService.GetProduct(line.ProductId, cancellationToken);
            if (!productInfo.IsSuccess()) continue;
            var product = productInfo.GetReuslt<ResponseProduct>();
            totalWidth += product.WidthInMillimeters * (line.Quantity / product.StackSize);
            totalWidth += product.WidthInMillimeters * (line.Quantity % product.StackSize);
        }

        return totalWidth;
    }

    public async Task<bool> OrderIsExist(int orderId, CancellationToken cancellationToken)
    {
        var result = await GetOrder(orderId, cancellationToken);
        return result.IsSuccess();
    }

}