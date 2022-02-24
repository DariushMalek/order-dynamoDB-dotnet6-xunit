using TechLand.Shop.Model.Entities;
using TechLand.Shop.Model.Response;

namespace TechLand.Shop.BusinessLogic.Mapper;

public interface IProductMapper
{
    IEnumerable<ResponseProduct> Convert(IEnumerable<Product> orderResponseModel);

    ResponseProduct Convert(Product productModel);
}

public  class ProductMapper : IProductMapper
{
    public IEnumerable<ResponseProduct> Convert(IEnumerable<Product> orderResponseModel)
    {
        return orderResponseModel.Select(Convert);
    }

    public ResponseProduct Convert(Product productModel)
    {
        var productResponseModel = new ResponseProduct
        {
            ProductId = productModel.ProductId,
            ProductName = productModel.ProductName,
            ProductTypeId = productModel.ProductTypeId,
            StackSize = productModel.StackSize,
            WidthInMillimeters = productModel.WidthInMillimeters
        };
        return productResponseModel;
    }
}