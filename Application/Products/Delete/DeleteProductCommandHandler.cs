using Application.Data;
using Domain.Exceptions;
using Domain.Products;
using MediatR;

namespace Application.Products.Delete;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork): IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.productId);

        if (product is null)
        {
            throw new ProductNotFoundException(request.productId);
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}