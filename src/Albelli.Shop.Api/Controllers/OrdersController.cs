using Albelli.Shop.BusinessLogic;
using Albelli.Shop.Model.Requests;
using Albelli.Shop.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace Albelli.Shop.Api.Controllers;

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

    [HttpGet("{oderId}")]
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