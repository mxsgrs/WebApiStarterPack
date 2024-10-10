namespace Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

public record UpdateOrderCommand(OrderId Id, UserId UserId, decimal TotalAmount, OrderStatus Status) : IRequest<UpdateOrderCommandResponse>;
