namespace Starter.Store.Application.Handlers.OrderHandlers.DeleteOrder;

public record DeleteOrderCommand(OrderId Id) : IRequest;
