namespace Starter.Store.WebApi.OrderFeature.Application;

public class OrderService(ILogger<OrderService> logger, IMapper mapper, 
    IOrderRepository orderRepository) : IOrderService
{
    private readonly ILogger<OrderService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        _logger.LogInformation("Create order for user {UserId}", orderDto.UserId);

        var order = _mapper.Map<Order>(orderDto);
        var createdOrder = await _orderRepository.CreateAsync(order);

        return _mapper.Map<OrderDto>(createdOrder);
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        _logger.LogInformation("Delete order with id {OrderId}", orderId);

        await _orderRepository.DeleteAsync(new OrderId(orderId));
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        _logger.LogInformation("Get order by id {OrderId}", orderId);

        var order = await _orderRepository.ReadAsync(new OrderId(orderId));
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<List<OrderDto>> GetOrdersByUserAsync(Guid userId)
    {
        _logger.LogInformation("Get orders for user {UserId}", userId);

        var orders = await _orderRepository.ReadByUserAsync(new UserId(userId));
        return _mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto> UpdateOrderAsync(Guid orderId, OrderDto orderDto)
    {
        _logger.LogInformation("Update order with id {OrderId}", orderId);

        var order = _mapper.Map<Order>(orderDto);
        var updatedOrder = await _orderRepository.UpdateAsync(new OrderId(orderId), order);

        return _mapper.Map<OrderDto>(updatedOrder);
    }
}
