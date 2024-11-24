namespace Application.Queries;

using Application.QueryResults;
using MediatR;

public record GetProductByIdQuery(int Id) : IRequest<ProductResult>;
