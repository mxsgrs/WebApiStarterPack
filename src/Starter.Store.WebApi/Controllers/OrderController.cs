using Microsoft.AspNetCore.Mvc;

namespace Starter.Store.WebApi.Controllers;

public class OrderController(IMapper mapper, IOrderRepository orderRepository) : StoreControllerBase(mapper)
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        Order order = _mapper.Map<Order>(orderDto);

        Order created = await _orderRepository.CreateAsync(order);

        OrderDto createdDto = _mapper.Map<OrderDto>(created);

        return Ok(createdDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _orderRepository.DeleteAsync(new(id));

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ReadOrder(Guid id)
    {
        Order order = await _orderRepository.ReadAsync(new(id));

        OrderDto orderDto = _mapper.Map<OrderDto>(order);

        return Ok(orderDto);
    }

    [HttpGet("/api/store/user/{userId:guid}/orders")]
    public async Task<IActionResult> ReadOrderByUser(Guid userId)
    {
        List<Order> orders = await _orderRepository.ReadByUserAsync(new(userId));

        List<OrderDto> orderDtos = orders.Select(_mapper.Map<OrderDto>).ToList();

        return Ok(orderDtos);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, OrderDto orderDto)
    {
        Order order = _mapper.Map<Order>(orderDto);

        Order updated = await _orderRepository.UpdateAsync(new(id), order);

        OrderDto updatedDto = _mapper.Map<OrderDto>(updated);

        return Ok(updatedDto);
    }
}
