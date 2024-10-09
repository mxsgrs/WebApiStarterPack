using Starter.Store.Domain.Aggregates.OrderAggregate;

namespace Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

public record CreateOrderCommandResponse(Guid OrderId, OrderStatus Status);
