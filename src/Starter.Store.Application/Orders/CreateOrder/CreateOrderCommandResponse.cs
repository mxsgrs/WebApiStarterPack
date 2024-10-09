using Starter.Store.Domain.Aggregates.OrderAggregate;

namespace Starter.Store.Application.Orders.CreateOrder;

public record CreateOrderCommandResponse(Guid OrderId, OrderStatus Status);
