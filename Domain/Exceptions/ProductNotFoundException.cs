using Domain.Products;

namespace Domain.Exceptions;

public class ProductNotFoundException(ProductId id): Exception($"Product with id {id.Value} was not found")
{
}