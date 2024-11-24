using Application.Queries;
using Application.QueryResults;
using Domain.Interfaces;
using MediatR;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResult>>
{
    private readonly IProductRepository productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await this.productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new ProductResult
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();
    }
}
