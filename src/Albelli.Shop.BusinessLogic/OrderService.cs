using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model.Mapper;
using Albelli.Shop.Model.Requests;
using Albelli.Shop.Model.Response;

namespace Albelli.Shop.BusinessLogic;

public interface IOrderService
{
    public Task CreatOrder(RequestOrder order, CancellationToken cancellationToken);

    public Task<ResponseOrder> GetOrder(int orderId, CancellationToken cancellationToken);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly OrderMapper _orderMapper;

    public OrderService(IOrderRepository orderRepository, OrderMapper orderMapper)
    {
        _orderRepository = orderRepository;
        _orderMapper = orderMapper;
    }

    public async Task CreatOrder(RequestOrder requestOrder, CancellationToken cancellationToken)
    {
        var order = _orderMapper.Convert(requestOrder);
        await _orderRepository.InsertAsync(order, cancellationToken);
    }

    public async Task<ResponseOrder> GetOrder(int orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByOrderIdAsync(orderId, cancellationToken);
        return _orderMapper.Convert(order);
    }
}