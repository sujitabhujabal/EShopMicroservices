using MediatR;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResult(ShoppingCart cart);
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}
