namespace Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository)
    : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        Order order = new(request.UserId, request.TotalAmount);

        Order created = await _orderRepository.CreateAsync(order);

        CreateOrderCommandResponse response = new(created.Id, created.Status);

        return response;
    }
}
