

namespace Basket.API.Basket.StoreBasket
{
    public record CreateCommandStoreBasket(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketValidator : AbstractValidator<CreateCommandStoreBasket>
    {
        public StoreBasketValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User Name is required");
        }
    }

    public class StoreBasketHandler : ICommandHandler<CreateCommandStoreBasket, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(CreateCommandStoreBasket command, CancellationToken cancellationToken)
        {
            //add to database
            return new StoreBasketResult("sujita");
        }
    }
}
