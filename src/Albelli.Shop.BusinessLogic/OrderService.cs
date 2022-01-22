using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model.Entities;
using Albelli.Shop.Model.Requests;
using Albelli.Shop.Model.Response;

namespace Albelli.Shop.BusinessLogic;

public interface IOrderService
{
    public Task CreateOrder(RequestOrder order, CancellationToken cancellationToken);

    public Task<ResponseOrder> GetOrder(int orderId, CancellationToken cancellationToken);
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

    public async Task CreateOrder(RequestOrder requestOrder, CancellationToken cancellationToken)
    {
        var order = _orderMapper.Convert(requestOrder);
        order.CreatedAt = DateTime.Now;
        order.StatusCode = EffectiveStatusCode.Active;
        order.RequiredBinWidthInMillimeters = await CalcRequiredBinWidth(order.Lines, cancellationToken);
        await _orderRepository.InsertAsync(order, cancellationToken);
    }

    public async Task<ResponseOrder> GetOrder(int orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByOrderIdAsync(orderId, cancellationToken);
        return _orderMapper.Convert(order);
    }

    private async Task<double> CalcRequiredBinWidth(List<OrderLine> orderLines, CancellationToken cancellationToken)
    {
        double totalWidth = 0;
        foreach (var line in orderLines)
        {
            var productInfo = await _productService.GetProduct(line.ProductId, cancellationToken);
            totalWidth += productInfo.WidthInMillimeters * (line.Quantity / productInfo.StackSize);
            totalWidth += productInfo.WidthInMillimeters * (line.Quantity % productInfo.StackSize);
        }

        return totalWidth;
    }

}