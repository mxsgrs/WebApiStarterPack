namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

public record GetOrdersByUserQueryResponse(Guid Id, Guid UserId, decimal TotalAmount, OrderStatus Status);