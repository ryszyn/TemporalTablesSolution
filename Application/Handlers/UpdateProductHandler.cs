namespace Application.Handlers;

using Application.Commands;
using Domain.Interfaces;
using MediatR;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await this.productRepository.GetAsync(request.ProductId, cancellationToken);

        if (existingProduct == null)
        {
            return false;
        }

        existingProduct.Name = request.Name;
        existingProduct.Price = request.Price;

        await this.productRepository.UpdateAsync(existingProduct, cancellationToken);

        return true;
    }
}
