using Starter.Store.Application.Handlers.OrderHandlers.GetOrdersByUser;

namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.GetOrdersByUser;

public class GetOrdersByUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/{userId:guid}/orders", async (IMapper mapper, IMediator mediator, 
            Guid userId, CancellationToken cancellationToken) =>
        {
            GetOrdersByUserQuery query = new(new(userId));

            List<GetOrdersByUserQueryResponse> orders = await mediator.Send(query, cancellationToken);


        });
    }
}
