namespace Application.Handlers;

using Domain.Entities;
using Domain.Interfaces;
using MediatR;

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
