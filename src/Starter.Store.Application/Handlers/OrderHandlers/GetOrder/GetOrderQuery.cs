namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

public record GetOrderQuery(Guid Id) : IRequest<GetOrderQueryResponse>;