namespace Application.Handlers;

using Application.Queries;
using Application.QueryResults;
using Domain.Interfaces;
using MediatR;

public class GetProductHistoryHandler : IRequestHandler<GetProductHistoryQuery, IEnumerable<ProductResult>>
{
    private readonly IProductRepository productRepository;

    public GetProductHistoryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResult>> Handle(GetProductHistoryQuery request, CancellationToken cancellationToken)
    {
        var history = await this.productRepository.GetHistoryAsync(request.ProductId, cancellationToken);

        return history.Select(p => new ProductResult
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();
    }
}
