using Microsoft.AspNetCore.Mvc;
using TechLand.Shop.BusinessLogic;
using TechLand.Shop.Model.Requests;
using TechLand.Shop.Model.Response;

namespace TechLand.Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(RequestOrder requestOrder, CancellationToken cancellationToken)
    {
        var result = await _orderService.CreateOrder(requestOrder, cancellationToken);

        if (result.IsSuccess())
        {
            return Ok();
        }

        return BadRequest(result.ValidationResult?.ErrorMessage);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<ResponseOrder>> GetAsync(int orderId, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetOrder(orderId, cancellationToken);

        if (result.IsSuccess())
        {
            return Ok(result.GetReuslt<ResponseOrder>());
        }

        return BadRequest(result.ValidationResult?.ErrorMessage);
    }
}