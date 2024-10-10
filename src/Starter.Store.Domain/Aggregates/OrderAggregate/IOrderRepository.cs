namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task DeleteAsync(OrderId id);
    Task<Order> ReadAsync(OrderId id);
    Task<List<Order>> ReadByUserAsync(UserId userId);
    Task<Order> UpdateAsync(OrderId id, Order order);
}
