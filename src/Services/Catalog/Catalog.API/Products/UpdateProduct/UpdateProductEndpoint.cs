using BuildingBlocks.CQRS;
using Carter;
using Catalog.API.Products.CreateProduct;
using Mapster;
using MediatR;
using System.Windows.Input;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommandRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
       :ICommand<UpdateProductCommandResponse>;
    public record UpdateProductCommandResponse(bool Updated);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products",
                async (UpdateProductCommandRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateProductCommandResponse>();

                    return Results.Ok(response);
                })
                .WithName("UpdateProduct")
                .Produces<UpdateProductCommandResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }
}
