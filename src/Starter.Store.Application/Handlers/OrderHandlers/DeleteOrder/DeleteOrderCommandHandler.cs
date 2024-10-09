namespace Starter.Store.Application.Handlers.OrderHandlers.DeleteOrder;

internal class DeleteOrderCommandHandler(IOrderRepository orderRepository) 
    : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task Handle(DeleteOrderCommand request, 
        CancellationToken cancellationToken)
    {
        await _orderRepository.DeleteAsync(request.Id);
    }
}
