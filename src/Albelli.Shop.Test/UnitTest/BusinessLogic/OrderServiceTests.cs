using System.Collections.Generic;
using System.Threading;
using Albelli.Shop.BusinessLogic;
using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model;
using Albelli.Shop.Model.Entities;
using Albelli.Shop.Model.Requests;
using Albelli.Shop.Model.Response;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Albelli.Shop.Test.UnitTest.BusinessLogic;

public class OrderServiceTests
{
    private readonly IFixture _fixture;

    private readonly IOrderService _orderService;

    private readonly IProductService _productService;

    private readonly IOrderRepository _orderRepository;

    private readonly IOrderMapper _orderMapper;

    public OrderServiceTests()

    {
        _fixture = new Fixture();

        _productService = Substitute.For<IProductService>();

        _orderRepository = Substitute.For<IOrderRepository>();

        _orderMapper = new OrderMapper();

        _orderService = new OrderService(_orderRepository, _orderMapper, _productService);
    }

    [Fact]
    public async void When_OrderIsExist_CreateOrder_ResponseHasError()
    {
        var orderId = _fixture.Create<int>();
        var order = _fixture.Build<Order>().With(n => n.OrderId, orderId).Create();
        var requestOrder = _fixture.Build<RequestOrder>().With(n => n.OrderId, orderId).Create();

        _orderRepository.GetOrderByIdAsync(orderId, Arg.Any<CancellationToken>()).Returns(order);

        var result = await _orderService.CreateOrder(requestOrder, CancellationToken.None);

        result.ValidationResult.Should().NotBeNull();
        result.ValidationResult?.ErrorMessage.Should().Be(Messages.OrderIsExist);
    }

    [Fact]
    public async void When_OrderHasNotAnyProduct_CreateOrder_ResponseHasError()
    {
        var requestOrder = _fixture.Build<RequestOrder>().With(n => n.Lines, new List<RequestOrderLine>()).Create();

        var result = await _orderService.CreateOrder(requestOrder, CancellationToken.None);

        result.ValidationResult.Should().NotBeNull();
        result.ValidationResult?.ErrorMessage.Should().Be(Messages.OrderHasNotAnyProduct);
    }

    [Fact]
    public async void When_ProductIsDuplicated_CreateOrder_ResponseHasError()
    {
        var requestOrderLine = _fixture.Create<RequestOrderLine>();
        var requestOrder = _fixture.Build<RequestOrder>().With(n => n.Lines, new List<RequestOrderLine>(){ requestOrderLine , requestOrderLine }).Create();

        var result = await _orderService.CreateOrder(requestOrder, CancellationToken.None);

        result.ValidationResult.Should().NotBeNull();
        result.ValidationResult?.ErrorMessage.Should().Be(Messages.ProductIsDuplicated);
    }

    [Fact]
    public async void HappyPath_CreateOrder()
    {
        var requestOrderLine1 = _fixture.Build<RequestOrderLine>().With(n => n.Quantity, 1).Create();
        var requestOrderLine2 = _fixture.Build<RequestOrderLine>().With(n => n.Quantity, 5).Create();
        var requestOrderLine3 = _fixture.Build<RequestOrderLine>().With(n => n.Quantity, 5).Create();

        var requestOrder = _fixture.Build<RequestOrder>().With(n => n.Lines, new List<RequestOrderLine> { requestOrderLine1, requestOrderLine2, requestOrderLine3 }).Create();

        var product1 = _fixture.Build<ResponseProduct>().With(n => n.ProductId, requestOrderLine1.ProductId)
            .With(n=>n.WidthInMillimeters, 19).With(n=>n.StackSize, 1).Create();
        var product2 = _fixture.Build<ResponseProduct>().With(n => n.ProductId, requestOrderLine2.ProductId)
            .With(n => n.WidthInMillimeters, 94).With(n => n.StackSize, 4).Create();

        _productService.GetProduct(requestOrderLine1.ProductId, Arg.Any<CancellationToken>()).Returns(new ResponseResult(product1));
        _productService.GetProduct(requestOrderLine2.ProductId, Arg.Any<CancellationToken>())
            .Returns(new ResponseResult(product2));
        _productService.GetProduct(requestOrderLine3.ProductId, Arg.Any<CancellationToken>())
            .Returns(new ResponseResult(Messages.ProductNotFound));

        var result = await _orderService.CreateOrder(requestOrder, CancellationToken.None);

        Assert.True(result.IsSuccess());
        result.Result.As<ResponseOrder>().OrderId.Should().Be(requestOrder.OrderId);
        result.Result.As<ResponseOrder>().RequiredBinWidthInMillimeters.Should().Be(207);
    }

    [Fact]
    public async void When_Order_NotFound()
    {
        var orderId = _fixture.Create<int>();
        var result = await _orderService.GetOrder(orderId, Arg.Any<CancellationToken>());

        result.ValidationResult.Should().NotBeNull();
        result.ValidationResult?.ErrorMessage.Should().Be(Messages.OrderNotFound);
    }

    [Fact]
    public async void HappyPath_GetOrder()
    {
        var orderId = _fixture.Create<int>();
        var order = _fixture.Build<Order>().With(n => n.OrderId, orderId).Create();

        _orderRepository.GetOrderByIdAsync(orderId, Arg.Any<CancellationToken>()).Returns(order);

        var result = await _orderService.GetOrder(orderId, CancellationToken.None);

        Assert.True(result.IsSuccess());
        result.Result.As<ResponseOrder>().OrderId.Should().Be(orderId);
    }
}