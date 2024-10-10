namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

public record GetOrderQueryResponse(OrderId Id, UserId UserId, decimal TotalAmount, OrderStatus Status);
