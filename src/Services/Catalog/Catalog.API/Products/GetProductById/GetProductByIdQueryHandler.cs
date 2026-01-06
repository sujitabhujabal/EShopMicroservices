using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Marten;

namespace Catalog.API.Products.GetProductById
{
    //query -request
    public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResuslt>;
    // result
    public record GetProductByIdResuslt(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResuslt>
    {
        public async Task<GetProductByIdResuslt> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler handled with query {@query}", query);
            var productById = await documentSession.LoadAsync<Product>(query.Id, cancellationToken);
            if (productById is null)
            {
                throw new NotFoundMessage("Product not found for Id: {id}" + query.Id);
            }
            else
                return new GetProductByIdResuslt(productById);
        }
    }
}
