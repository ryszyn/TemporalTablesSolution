namespace Application.Handlers;

using Application.QueryResults;
using Domain.Interfaces;
using MediatR;
using Product = Domain.Entities.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        return await this.productRepository.GetAsync(request.Id, cancellationToken);
    }
}
