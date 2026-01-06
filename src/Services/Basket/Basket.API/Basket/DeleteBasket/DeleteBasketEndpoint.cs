
using Basket.API.Basket.GetBasket;
using Mapster;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteCommandShoppingCartRequest(Guid Guid);
    public record DeleteCommandShoppingCartResponse(bool succeeded);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{Username}", async (string username, ISender sender) => {
                var command = sender.Send(new DeleteCommandShoppingCart(username));
                var response = command.Adapt<DeleteCommandShoppingCartResponse>();
                return Results.Ok(response);

            })
                .WithName("Delete Shopping Cart")
                .Produces<DeleteCommandShoppingCartResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Shopping Cart").
            WithDescription("Delete Shoping Cart");
        }
    }
}
