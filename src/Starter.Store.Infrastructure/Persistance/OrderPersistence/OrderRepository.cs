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

    public async Task<Order> GetAsync(Guid id)
    {
        _logger.LogInformation("Read order {Id} in database", id);

        Order order = await _dbContext.Orders.FindAsync(id)
            ?? throw new OrderNotFoundException(id);

        return order;
    }

    public async Task<List<Order>> GetByUserAsync(Guid userId)
    {
        _logger.LogInformation("Read orders for user {UserId} in database", userId);

        List<Order> orders = await _dbContext.Orders
            .Where(order => order.UserId == userId)
            .ToListAsync()
                ?? throw new OrdersByUserNotFoundException(userId);

        return orders;
    }

    public async Task<Order> UpdateAsync(Guid id, Order order)
    {
        _logger.LogInformation("Update order {Id} in database", id);

        Order existing = await _dbContext.Orders.FindAsync(id)
            ?? throw new OrderNotFoundException(id);

        order.Id = existing.Id;
        _dbContext.Entry(existing).CurrentValues.SetValues(order);
        await _dbContext.SaveChangesAsync();

        return existing;
    }
}
