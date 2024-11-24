using Domain.Entities;
using MediatR;

public class GetProductByIdQuery : IRequest<Product>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        this.Id = id;
    }
}
