
using Microsoft.Extensions.Logging;

namespace Cataolg.API.Products.CreateProduct
{

    public record GetProductsByCategoryHandlerQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);



    public class GetProductsByCategoryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductsByCategoryHandlerQuery, GetProductsByCategoryResult>
    {


        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryHandlerQuery query, CancellationToken cancellationToken)
        {

            var products = await session.Query<Product>().Where(c=>c.Category.Contains(query.Category)).ToListAsync();

            

            return new GetProductsByCategoryResult(products);
        }



    }
}
