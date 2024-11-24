namespace Application.Queries;

using Application.QueryResults;
using MediatR;

public record GetAllProductsQuery : IRequest<IEnumerable<Product>>;
