
using Mapster;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart cart);
    public record StoreBasketResponse(string Username);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/", async(StoreBasketCommand request, ISender sender) => {
               var command = request.Adapt<StoreBasketCommand>();
               var result = await sender.Send(command);
               var response = result.Adapt<StoreBasketResponse>();

               return Results.Created($"/basket/{response.Username}", response);
            })             
            .WithName("Store Shopping Cart")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Shopping Cart").
            WithDescription("Store Shopping Cart");

        }
    }
}
