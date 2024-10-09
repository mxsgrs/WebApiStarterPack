using Starter.Store.Application.Handlers.OrderHandlers.CreateOrder;

namespace Starter.Store.WebApi.Endpoints.Orders.CreateOrder;

public class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/order", async (IMapper mapper, IMediator mediator, 
            CreateOrderRequest request, CancellationToken cancellationToken) =>
        {
            CreateOrderCommand command = request.Adapt<CreateOrderCommand>();

            CreateOrderCommandResponse commandResponse = await mediator.Send(command, cancellationToken);

            CreateOrderResponse response = commandResponse.Adapt<CreateOrderResponse>();

            return Results.Ok(response);
        });
    }
}
