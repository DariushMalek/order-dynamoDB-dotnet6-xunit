using Albelli.Shop.BusinessLogic.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Albelli.Shop.BusinessLogic;
public static class Configuration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        return services.AddTransient<IOrderMapper, OrderMapper>()
            .AddTransient<IProductMapper, ProductMapper>()
            .AddTransient<IOrderService, OrderService>()
            .AddTransient<IProductService, ProductService>();
    }
}

