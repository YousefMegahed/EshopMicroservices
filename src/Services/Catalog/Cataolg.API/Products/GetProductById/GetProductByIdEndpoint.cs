﻿

namespace Cataolg.API.Products.CreateProduct
{

    public record GetProductByIdResponse(Product Product);


    public class GetProductByIdEndpoint : ICarterModule
    {
        public async void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/product/{id}", async (Guid id , ISender sender) =>
            {

                var result = await sender.Send(new GetProductByIdHandlerQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);

            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product");

        }
    }
}