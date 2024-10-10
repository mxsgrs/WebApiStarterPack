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

    public async Task DeleteAsync(OrderId id)
    {
        _logger.LogInformation("Delete order {Id} in database", id.Value);

        Order order = await _dbContext.Orders.FindAsync(id.Value) 
            ?? throw new OrderNotFoundException(id.Value);

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order> GetAsync(OrderId id)
    {
        _logger.LogInformation("Read order {Id} in database", id.Value);

        Order order = await _dbContext.Orders.FindAsync(id.Value)
            ?? throw new OrderNotFoundException(id.Value);

        return order;
    }

    public async Task<List<Order>> GetByUserAsync(UserId userId)
    {
        _logger.LogInformation("Read orders for user {UserId} in database", userId.Value);

        List<Order> orders = await _dbContext.Orders
            .Where(order => order.UserId.Value == userId.Value)
            .ToListAsync()
                ?? throw new OrdersByUserNotFoundException(userId.Value);

        return orders;
    }

    public async Task<Order> UpdateAsync(OrderId id, Order order)
    {
        _logger.LogInformation("Update order {Id} in database", id.Value);

        Order existing = await _dbContext.Orders.FindAsync(id)
            ?? throw new OrderNotFoundException(id.Value);

        existing.UpdateStatus(order.Status);
        _dbContext.Update(existing);
        await _dbContext.SaveChangesAsync();

        return existing;
    }
}
