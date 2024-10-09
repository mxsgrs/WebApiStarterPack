namespace Starter.Store.WebApi.Endpoints.Orders.CreateOrder;

public class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (IMapper mapper, IMediator mediator, 
            CreateOrderRequest request, CancellationToken cancellationToken) =>
        {
            CreateOrderCommand command = mapper.Map<CreateOrderCommand>(request);

            CreateOrderCommandResponse commandResponse = await mediator.Send(command, cancellationToken);

            CreateOrderResponse response = mapper.Map<CreateOrderResponse>(commandResponse);

            return response;
        });
    }
}
