namespace Starter.Store.Domain.Aggregates.OrderAggregate;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);
    Task DeleteAsync(Guid id);
    Task<Order> GetAsync(Guid id);
    Task<List<Order>> GetByUserAsync(Guid userId);
    Task<Order> UpdateAsync(Guid id, Order order);
}
