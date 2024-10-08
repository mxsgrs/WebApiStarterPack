namespace Starter.Store.Application.Orders.CreateOrder;

public record CreateOrderCommand(Guid UserId, decimal TotalAmount) : IRequest<CreateOrderCommandResponse>;
