using System.Threading;
using Albelli.Shop.BusinessLogic;
using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model;
using Albelli.Shop.Model.Entities;
using Albelli.Shop.Model.Response;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Albelli.Shop.Test.UnitTest.BusinessLogic;

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