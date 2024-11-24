namespace Application.Handlers;

using Application.Commands;
using Domain.Interfaces;
using MediatR;
using Product = Domain.Entities.Product;

internal sealed class AddProductHandler : IRequestHandler<AddProduct>
{
    private readonly IPublisher mediator;
    private readonly IProductRepository repository;

    public AddProductHandler(IPublisher mediator, IProductRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(AddProduct request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price,
        };

        await this.repository.AddAsync(product, cancellationToken);
    }
}
