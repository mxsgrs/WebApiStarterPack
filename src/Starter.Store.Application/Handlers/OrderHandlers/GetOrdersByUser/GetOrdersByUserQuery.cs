namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

public record GetOrdersByUserQuery(Guid UserId) : IRequest<List<GetOrdersByUserQueryResponse>>;