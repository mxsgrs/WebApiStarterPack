namespace Starter.Store.WebApi.OrderFeature.Domain;

public class OrderNotFoundException(Guid id) : NotFoundException($"Order {id} was not found.") { }
public class OrdersByUserNotFoundException(Guid id) : NotFoundException($"No order for user {id} was found.") { }