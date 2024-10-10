namespace Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

public record CreateOrderCommandResponse(OrderId OrderId, OrderStatus Status);
