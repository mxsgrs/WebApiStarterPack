using Starter.Store.Domain.OrderAggregate;

namespace Starter.Store.Infrastructure.Persistance.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<Order> CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetByUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateAsync(Guid id, Order order)
    {
        throw new NotImplementedException();
    }
}
