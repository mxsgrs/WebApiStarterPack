namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

public record GetOrdersByUserQueryResponse(OrderId Id, UserId UserId, decimal TotalAmount, OrderStatus Status);