using BuildingBlocks.CQRS;
using Catalog.API.Exceptions;
using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;
using FluentValidation;
using Marten;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
             : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Updated);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .Length(1, 30).WithMessage("Name must be in between 1 and 30");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler executed");
            var result = await documentSession.LoadAsync<Product>(command.Id,cancellationToken);
            if (result is null)
            {
                throw new NotFoundMessage("Product not found");
            }
            result.Name = command.Name;
            result.Category = command.Category;
            result.Description = command.Description;
            result.ImageFile = command.ImageFile;
            result.Price = command.Price;

            documentSession.Update(result);
            await documentSession.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);

        }
    }
}
