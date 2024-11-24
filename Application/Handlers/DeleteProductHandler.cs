namespace Application.Handlers;

using Application.Commands;
using Domain.Interfaces;
using MediatR;

internal sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IPublisher mediator;
    private readonly IProductRepository repository;

    public DeleteProductHandler(IPublisher mediator, IProductRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var deleteProduct = new DeleteProductCommand
        {
            Id = request.Id,
        };

        await this.repository.DeleteAsync(deleteProduct.Id, cancellationToken);
    }
}
