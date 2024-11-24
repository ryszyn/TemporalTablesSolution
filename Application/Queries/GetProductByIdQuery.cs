namespace Application.Queries;

using Application.QueryResults;
using MediatR;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResult>;
