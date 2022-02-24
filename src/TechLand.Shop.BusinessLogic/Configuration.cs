using Microsoft.Extensions.DependencyInjection;
using TechLand.Shop.BusinessLogic.Mapper;

namespace TechLand.Shop.BusinessLogic;
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

