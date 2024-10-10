namespace Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

public record CreateOrderCommand(UserId UserId, decimal TotalAmount) : IRequest<CreateOrderCommandResponse>;
