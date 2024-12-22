
using Microsoft.Extensions.Logging;

namespace Cataolg.API.Products.CreateProduct
{

    public record GetProductByIdHandlerQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);



    public class GetProductByIdHandler(IDocumentSession session,ILogger<GetProductByIdHandlerQuery> logger) : IQueryHandler<GetProductByIdHandlerQuery, GetProductByIdResult>
    {

        public async Task<GetProductByIdResult> Handle(GetProductByIdHandlerQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdHandlerQuery.Handle called with {@Query}");
            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

            if (product is null)
                throw new ProductNotFoundException();

            return new GetProductByIdResult(product);

        }

    }
}
