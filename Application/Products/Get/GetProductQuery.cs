using Domain.Products;
using MediatR;

namespace Application.Products.Get;

public record GetProductQuery(ProductId productId): IRequest<GetProductResponse>;

public record GetProductResponse(
    Guid Id,
    string Name,
    string Sku,
    string Currency,
    decimal Amount);