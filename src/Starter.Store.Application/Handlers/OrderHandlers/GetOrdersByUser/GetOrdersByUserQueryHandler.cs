namespace Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

public class GetOrdersByUserQueryHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetOrdersByUserQuery, List<GetOrdersByUserQueryResponse>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<List<GetOrdersByUserQueryResponse>> Handle(GetOrdersByUserQuery request, 
        CancellationToken cancellationToken)
    {
        List<Order> orders = await _orderRepository.GetByUserAsync(request.UserId);

        List<GetOrdersByUserQueryResponse> responses = orders
            .Select(order => new GetOrdersByUserQueryResponse(order.Id, order.UserId, order.TotalAmount, order.Status))
            .ToList();

        return responses;
    }
}
