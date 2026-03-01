namespace Domain.Products;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id);
    void Add(Product product);
    void Delete(Product product);
    Product Update(Product product); 
}