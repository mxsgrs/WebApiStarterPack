namespace Starter.Store.WebApi.Endpoints.OrderEndpoints.GetOrdersByUser;

public class GetOrdersByUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/{userId:guid}/orders", async (IMapper mapper, IMediator mediator) =>
        {

        });
    }
}
