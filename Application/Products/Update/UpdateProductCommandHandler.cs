using Application.Data;
using Domain.Exceptions;
using Domain.Products;
using MediatR;

namespace Application.Products.Update;

public class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork): IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.product.Id);

        if (product is null)
        {
            throw new ProductNotFoundException(request.product.Id);
        }

        productRepository.Update(request.product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}