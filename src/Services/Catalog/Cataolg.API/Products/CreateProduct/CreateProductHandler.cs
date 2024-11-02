namespace Cataolg.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);



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
