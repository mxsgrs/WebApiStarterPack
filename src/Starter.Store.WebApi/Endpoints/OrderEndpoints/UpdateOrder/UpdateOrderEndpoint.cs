using Starter.Store.Application.Handlers.OrderHandlers.UpdateOrder;

namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.UpdateOrder;

public class UpdateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id:guid}", async (IMapper mapper, IMediator mediator, Guid id,
            UpdateOrderRequest request, CancellationToken cancellationToken) =>
        {
            UpdateOrderCommand command = new();

            UpdateOrderCommandResponse response = await mediator.Send(command, cancellationToken);
        });
    }
}
