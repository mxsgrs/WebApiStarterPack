namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

public record GetOrderQueryResponse(Guid Id, Guid UserId, decimal TotalAmount, OrderStatus Status);
