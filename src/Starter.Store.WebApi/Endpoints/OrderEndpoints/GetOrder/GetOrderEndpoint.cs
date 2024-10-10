using Starter.Store.Application.Handlers.OrderHandlers.GetOrder;

namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.GetOrder;

public class GetOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{id:guid}", async (IMapper mapper, IMediator mediator, Guid id) =>
        {
            GetOrderQuery query = new(new(id));

            GetOrderQueryResponse queryResponse = await mediator.Send(query);

            GetOrderResponse response = new(new)
        });
    }
}
