using Albelli.Shop.BusinessLogic.Mapper;
using Albelli.Shop.Data.Repositories;
using Albelli.Shop.Model;
using Albelli.Shop.Model.Response;

namespace Albelli.Shop.BusinessLogic;

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
        var product = await _productRepository.GetByProductIdAsync(productId, cancellationToken);
        if (product == null)
        {
            return new ResponseResult(Messages.ProductNotFound);
        }
        return new ResponseResult(_productMapper.Convert(product));
    }
}