namespace Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

internal class UpdateOrderCommandHandler(IOrderRepository orderRepository) 
    : IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        Order order = new(new OrderId(Guid.NewGuid()), request.UserId, request.TotalAmount);

        Order updated = await _orderRepository.UpdateAsync(request.Id, order);

        UpdateOrderCommandResponse response = new(updated.Id, updated.UserId, 
            updated.TotalAmount, updated.Status);

        return response;
    }
}
