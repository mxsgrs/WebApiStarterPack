using Starter.Store.Domain.Aggregates.OrderAggregate;

namespace Starter.Store.Application.Orders.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository) 
    : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        Order order = new()
        {
            UserId = request.UserId,
            TotalAmount = request.TotalAmount,
            Status = OrderStatus.Pending,
            CreationDate = DateTime.UtcNow
        };

        Order newOrder = await _orderRepository.CreateAsync(order);

        CreateOrderCommandResponse response = new(newOrder.Id, newOrder.Status);

        return response;
    }
}
