using Domain.Products;
using MediatR;

namespace Application.Products.Update;

public record UpdateProductCommand(Product product) : IRequest;

public record UpdateProductRequest (ProductId productId, string name, decimal amount, string currency, string sku);