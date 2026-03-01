using Application.Data;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get;

public class GetProductQueryHandler(IApplicationDbContext context): IRequestHandler<GetProductQuery, GetProductResponse>
{
    public async Task<GetProductResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await context.Products.Where(x => x.Id == query.productId)
            .Select(x => new GetProductResponse(x.Id.Value, x.Name, x.Sku.Value, x.Price.Currency, x.Price.Amount))
            .FirstOrDefaultAsync(cancellationToken);

        return product ?? throw new ProductNotFoundException(query.productId);
    }
}