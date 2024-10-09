using Microsoft.Extensions.Logging;

namespace Starter.Store.Infrastructure.Persistance.OrderPersistence;

public class OrderRepository(ILogger<OrderRepository> logger,
    StoreDbContext dbContext) : IOrderRepository
{
    private readonly ILogger<OrderRepository> _logger = logger;
    private readonly StoreDbContext _dbContext = dbContext;

    public async Task<Order> CreateAsync(Order order)
    {
        _logger.LogInformation("Add new order in database {Order}", order);

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Delete order {Id} in database", id);

        Order order = await _dbContext.Orders.FindAsync(id) 
            ?? throw new OrderNotFoundException(id);

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
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
