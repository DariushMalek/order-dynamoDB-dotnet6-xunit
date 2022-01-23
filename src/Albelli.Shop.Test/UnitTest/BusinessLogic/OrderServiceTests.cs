using Albelli.Shop.BusinessLogic;
using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using AutoFixture;
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

        _orderMapper = Substitute.For<IOrderMapper>();

        _orderService = new OrderService(_orderRepository, _orderMapper, _productService);
    }

    [Fact]
    public void When_OrderIsExist_CreateOrder_ResponseHasError()
    {

    }

    [Fact]
    public void When_OrderHasNotAnyProduct_CreateOrder_ResponseHasError()
    {

    }

    [Fact]
    public void When_ProductIsDuplicated_CreateOrder_ResponseHasError()
    {

    }

    [Fact]
    public void CalcRequiredBinWidthTest()
    {

    }

    [Fact]
    public void HappyPath_CreateOrder()
    {

    }

    [Fact]
    public void When_Order_NotFound()
    {

    }

    [Fact]
    public void HappyPath_GetOrder()
    {

    }
}