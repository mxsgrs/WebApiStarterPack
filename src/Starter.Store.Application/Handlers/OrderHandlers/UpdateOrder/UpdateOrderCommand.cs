namespace Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

public record UpdateOrderCommand(Guid Id, Guid UserId, decimal TotalAmount, OrderStatus Status) : IRequest<UpdateOrderCommandResponse>;
