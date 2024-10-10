namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

public record GetOrdersByUserQuery(UserId UserId) : IRequest<List<GetOrdersByUserQueryResponse>>;