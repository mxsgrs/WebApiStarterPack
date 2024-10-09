namespace Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

public record CreateOrderCommand(Guid UserId, decimal TotalAmount) : IRequest<CreateOrderCommandResponse>;
