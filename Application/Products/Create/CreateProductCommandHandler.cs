using Application.Data;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

public class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork): IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(new ProductId(Guid.NewGuid()), command.name, new Money(command.currency, command.amount), Sku.Create(command.sku)!);
        
        productRepository.Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}