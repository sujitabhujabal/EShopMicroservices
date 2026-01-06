using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;
using Marten;
using MediatR;
using System.Windows.Input;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateCommandProduct(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
             : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator: AbstractValidator<CreateCommandProduct>
    {
        public CreateProductCommandValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than zero.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file name is required");
        }
    }

    internal class CreateProductHandler(IDocumentSession documentSession) : ICommandHandler<CreateCommandProduct, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateCommandProduct command, CancellationToken cancellationToken)
        {
            //Add business logic to create product

            //Create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };
            //Save to database
            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            //Return CreateProductResult as result
            return new CreateProductResult(product.Id);
            
            throw new NotImplementedException();
        }
    }
}
