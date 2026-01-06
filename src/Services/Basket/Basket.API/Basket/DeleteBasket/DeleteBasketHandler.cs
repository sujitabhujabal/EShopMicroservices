
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteCommandShoppingCart(string Username): ICommand<DeleteShoppingCartResponse>;
    public record DeleteShoppingCartResponse(bool succeeded);

    public class DeleteBasketHandlerValidator : AbstractValidator<DeleteCommandShoppingCart>
    {
        public DeleteBasketHandlerValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBasketHandler : ICommandHandler<DeleteCommandShoppingCart, DeleteShoppingCartResponse>
    {
        public async Task<DeleteShoppingCartResponse> Handle(DeleteCommandShoppingCart request, CancellationToken cancellationToken)
        {
            return new DeleteShoppingCartResponse(true);
        }
    }
}
