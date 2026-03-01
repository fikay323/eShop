using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.Update;
using Carter;
using Domain.Exceptions;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Endpoints;

public class Products: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            return await HandleProductNotFound(async () =>
            {
                var command = new DeleteProductCommand(new ProductId(id));
                await sender.Send(command);

                return Results.NoContent();
            });
        });

        app.MapPut("products/id:guid", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            return await HandleProductNotFound(async () =>
            {
                var command = new UpdateProductCommand(new Product(
                    new ProductId(id),
                    request.name,
                    new Money(request.currency, request.amount),
                    Sku.Create(request.sku)!));

                await sender.Send(command);
                return Results.Ok();
            });
        });

        app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            return await HandleProductNotFound(async () =>
            {
                var product = await sender.Send(new GetProductQuery(new ProductId(id)));
                return Results.Ok(product);
            });
        });
    }

    private static async Task<IResult> HandleProductNotFound(Func<Task<IResult>> action)
    {
        try
        {
            return await action();
        }
        catch (ProductNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
}