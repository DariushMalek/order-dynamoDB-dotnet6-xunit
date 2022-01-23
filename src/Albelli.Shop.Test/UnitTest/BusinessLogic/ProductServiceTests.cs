using Albelli.Shop.BusinessLogic;
using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using AutoFixture;
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

        _productMapper = Substitute.For<IProductMapper>();

        _productService = new ProductService(_productRepository, _productMapper);
    }

    [Fact]
    public void When_Product_NotFound()
    {

    }

    [Fact]
    public void HappyPath()
    {

    }
}