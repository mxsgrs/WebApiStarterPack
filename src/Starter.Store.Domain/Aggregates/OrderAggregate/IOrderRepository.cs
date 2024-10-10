namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task DeleteAsync(OrderId id);
    Task<Order> GetAsync(OrderId id);
    Task<List<Order>> GetByUserAsync(UserId userId);
    Task<Order> UpdateAsync(OrderId id, Order order);
}
