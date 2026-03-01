using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(string name, string sku, string currency, decimal amount): IRequest;