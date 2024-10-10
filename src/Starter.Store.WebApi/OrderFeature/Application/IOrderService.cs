
namespace Starter.Store.WebApi.OrderFeature.Application
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task DeleteOrderAsync(Guid orderId);
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<List<OrderDto>> GetOrdersByUserAsync(Guid userId);
        Task<OrderDto> UpdateOrderAsync(Guid orderId, OrderDto orderDto);
    }
}