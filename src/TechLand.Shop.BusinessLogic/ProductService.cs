using TechLand.Shop.BusinessLogic.Mapper;
using TechLand.Shop.Data.Repositories;
using TechLand.Shop.Model;
using TechLand.Shop.Model.Response;

namespace TechLand.Shop.BusinessLogic;

public interface IProductService
{
    Task<ResponseResult> GetProduct(int productId, CancellationToken cancellationToken);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    private readonly IProductMapper _productMapper;

    public ProductService(IProductRepository productRepository, IProductMapper productMapper)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
    }

    public async Task<ResponseResult> GetProduct(int productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
        if (product == null)
        {
            return new ResponseResult(Messages.ProductNotFound);
        }
        return new ResponseResult(_productMapper.Convert(product));
    }
}