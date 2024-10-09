namespace Starter.Store.Infrastructure.Persistance.OrderPersistence;

public class OrderNotFoundException(Guid id) : Exception($"Order {id} was not found.") { }