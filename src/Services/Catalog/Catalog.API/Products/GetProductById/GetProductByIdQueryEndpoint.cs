using Carter;
using Catalog.API.Models;
using Catalog.API.Products.GetProducts;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductsByIdRequest(Guid Id);
    public record GetProductByIdQueryResponse(Product Product);
    public class GetProductByIdQueryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{Id}", async(Guid Id, ISender sender) => {
               
                var product = await sender.Send(new GetProductByIdQuery(Id));
                var response = product.Adapt<GetProductByIdQueryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductsById")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id").
            WithDescription("Get Products By Id");
        }
    }
}
