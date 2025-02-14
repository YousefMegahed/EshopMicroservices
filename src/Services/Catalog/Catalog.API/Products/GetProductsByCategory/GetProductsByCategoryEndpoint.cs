

namespace Cataolg.API.Products.CreateProduct
{

    public record GetProductsByCategoryResponse(IEnumerable<Product> Products);


    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {

                var result = await sender.Send(new GetProductsByCategoryHandlerQuery(category));

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);

            })
                .WithName("GetProductsByCategory")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products By Category")
                .WithDescription("Get Products By Category");

        }
    }
}
