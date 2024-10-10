namespace Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

public record UpdateOrderCommandResponse(OrderId Id, UserId UserId, decimal TotalAmount, OrderStatus Status);