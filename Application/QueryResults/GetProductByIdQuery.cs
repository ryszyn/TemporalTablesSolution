using MediatR;

namespace Application.QueryResults;

public class GetProductByIdQuery : IRequest<Domain.Entities.Product>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        this.Id = id;
    }
}