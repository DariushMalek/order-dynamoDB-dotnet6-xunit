using System.Threading;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using TechLand.Shop.BusinessLogic;
using TechLand.Shop.BusinessLogic.Mapper;
using TechLand.Shop.Data.Repositories;
using TechLand.Shop.Model;
using TechLand.Shop.Model.Entities;
using TechLand.Shop.Model.Response;
using Xunit;

namespace TechLand.Shop.Test.UnitTest.BusinessLogic;

public class ProductServiceTests
{
    private readonly IFixture _fixture;

    private readonly IProductService _productService;

    private readonly IProductRepository _productRepository;

    private readonly IProductMapper _productMapper;

    public ProductServiceTests()

    {
        _fixture = new Fixture();

        _productRepository = Substitute.For<IProductRepository>();

        _productMapper = new ProductMapper();

        _productService = new ProductService(_productRepository, _productMapper);
    }

    [Fact]
    public async void When_Product_NotFound()
    {
        var productId = _fixture.Create<int>();
        var result = await _productService.GetProduct(productId, Arg.Any<CancellationToken>());

        result.ValidationResult.Should().NotBeNull();
        result.ValidationResult?.ErrorMessage.Should().Be(Messages.ProductNotFound);
    }

    [Fact]
    public async void HappyPath()
    {
        var productId = _fixture.Create<int>();
        var product = _fixture.Build<Product>().With(n => n.ProductId, productId).Create();

        _productRepository.GetProductByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);

        var result = await _productService.GetProduct(productId, CancellationToken.None);

        Assert.True(result.IsSuccess());
        result.Result.As<ResponseProduct>().ProductId.Should().Be(productId);
    }
}