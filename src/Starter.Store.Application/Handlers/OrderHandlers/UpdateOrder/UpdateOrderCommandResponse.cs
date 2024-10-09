namespace Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

public record UpdateOrderCommandResponse(Guid Id, Guid UserId, decimal TotalAmount, OrderStatus Status);