namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

public class GetOrderQueryHandler(IOrderRepository orderRepository) 
    : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, 
        CancellationToken cancellationToken)
    {
        Order order = await _orderRepository.GetAsync(request.Id);

        GetOrderQueryResponse response = new(order.Id, order.UserId, order.TotalAmount, order.Status);

        return response;
    }
}
