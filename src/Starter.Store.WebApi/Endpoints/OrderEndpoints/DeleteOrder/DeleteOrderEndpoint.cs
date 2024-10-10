using Starter.Store.Application.Handlers.OrderHandlers.DeleteOrder;

namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.DeleteOrder;

public class DeleteOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/order/{id:guid}", async (IMapper mapper, IMediator mediator, 
            Guid id, CancellationToken cancellationToken) =>
        {
            DeleteOrderCommand command = new(new(id));

            await mediator.Send(command, cancellationToken);
        });
    }
}
