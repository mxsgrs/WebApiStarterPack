namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.DeleteOrder;

public class DeleteOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/order/{id:guid}", async (IMapper mapper, IMediator mediator, Guid id) =>
        {
            OrderId orderId = new(id);
            DeleteOrderCommand command = new(orderId);

            await mediator.Send(command);
        });
    }
}
