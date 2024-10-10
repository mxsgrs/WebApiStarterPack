using Microsoft.AspNetCore.Mvc;
using Starter.Store.WebApi.OrderFeature.Application;

namespace Starter.Store.WebApi.OrderFeature.Presentation;

public class OrderController(IMapper mapper, IOrderService orderService) : StoreControllerBase(mapper)
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        OrderDto createdDto = await _orderService.CreateOrderAsync(orderDto);

        return Ok(createdDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _orderService.DeleteOrderAsync(id);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ReadOrder(Guid id)
    {
        OrderDto orderDto = await _orderService.GetOrderByIdAsync(id);

        return Ok(orderDto);
    }

    [HttpGet("/api/store/user/{userId:guid}/orders")]
    public async Task<IActionResult> ReadOrderByUser(Guid userId)
    {
        List<OrderDto> orderDtos = await _orderService.GetOrdersByUserAsync(userId);

        return Ok(orderDtos);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, OrderDto orderDto)
    {
        OrderDto updatedDto = await _orderService.UpdateOrderAsync(id, orderDto);

        return Ok(updatedDto);
    }
}

