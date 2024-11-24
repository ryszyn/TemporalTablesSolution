namespace Application.Handlers;

using Application.Queries;
using Application.QueryResults;
using Domain.Interfaces;
using MediatR;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResult?>
{
    private readonly IProductRepository productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ProductResult?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(request.Id, cancellationToken);

        if (product == null)
        {
            return default;
        }

        var result = new ProductResult
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ValidFrom = product.ValidFrom,
            ValidTo = product.ValidTo,
        };

        return result;
    }
}
