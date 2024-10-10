namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

public record GetOrderQuery(OrderId Id) : IRequest<GetOrderQueryResponse>;