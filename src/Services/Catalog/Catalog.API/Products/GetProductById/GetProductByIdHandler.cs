using Microsoft.Extensions.Logging;

namespace Cataolg.API.Products.CreateProduct
{

    public record GetProductByIdHandlerQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);



    public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdHandlerQuery, GetProductByIdResult>
    {

        public async Task<GetProductByIdResult> Handle(GetProductByIdHandlerQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(query.Id);

            return new GetProductByIdResult(product);

        }

    }
}
