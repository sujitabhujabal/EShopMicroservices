using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductByCaterory
{
    public record GetProductByCateroryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCateroryQueryHandler(IDocumentSession documentSession, ILogger<GetProductByCateroryQueryHandler> logger) 
        : IQueryHandler<GetProductByCateroryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCateroryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCateroryQueryHandler executed with the category", query.Category);
           var result = await documentSession.Query<Product>().Where(k=>k.Category.Contains(query.Category)).ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(result);
        }
    }
}
