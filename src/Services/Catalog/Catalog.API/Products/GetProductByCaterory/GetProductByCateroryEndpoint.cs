using Carter;
using Catalog.API.Models;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductByCaterory
{
    //public record GetProductByCateroryRequest;
    public record GetProductByCateroryResponse(IEnumerable<Product> Products);

    public class GetProductByCateroryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{Category}", async (string Category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCateroryQuery(Category));
                    var response = result.Adapt<GetProductByCateroryResponse>();
                    return Results.Ok(response);
                }).WithName("GetProductByCaterory")
            .Produces<GetProductByCateroryResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductByCaterory")
            .WithDescription("GetProductByCaterory");

        }
    }
}
