using Carter;
using Catalog.API.Products.UpdateProduct;
using Mapster;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool Deleted);
    public class DeleteProductCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) => { 

                var command = await sender.Send(new DeleteProductCommand(Id));
                var res = command.Adapt<DeleteProductResponse>();
                return Results.Ok(res);

            }).WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
