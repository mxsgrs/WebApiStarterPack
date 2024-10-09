
namespace Starter.Store.WebApi.Endpoints.Orders.DeleteOrder
{
    public class DeleteOrderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/order/{id:guid}", async(IMapper mapper, IMediator mediator) =>
            {
                mediator.Send
            });
        }
    }
}
