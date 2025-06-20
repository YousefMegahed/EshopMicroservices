﻿namespace Cataolg.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required !");
            RuleFor(c => c.Category).NotEmpty().WithMessage("Category is required !");
            RuleFor(c => c.ImageFile).NotEmpty().WithMessage("ImageFile is required !");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {


            var product = new Product
            {
                Category = command.Category,
                Description = command.Description,
                Name = command.Name,
                Price = command.Price,
                ImageFile = command.ImageFile
            };


            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);


            // logic here 
            return new CreateProductResult(product.Id);
        }
    }
}
