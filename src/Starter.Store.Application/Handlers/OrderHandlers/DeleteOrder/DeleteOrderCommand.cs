namespace Starter.Store.Application.Handlers.OrderHandlers.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest;
