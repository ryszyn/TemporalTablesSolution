namespace Application.Handlers;

using Application.Commands;
using Domain.Interfaces;
using MediatR;

internal sealed class DeleteProductHandler : IRequestHandler<DeleteProduct>
{
    private readonly IPublisher mediator;
    private readonly IProductRepository repository;

    public DeleteProductHandler(IPublisher mediator, IProductRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(DeleteProduct request, CancellationToken cancellationToken)
    {
        var deleteProduct = new DeleteProduct
        {
            Id = request.Id,
        };

        await this.repository.DeleteAsync(deleteProduct.Id, cancellationToken);
    }
}
