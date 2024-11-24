namespace Application.Queries;

using Application.QueryResults;
using MediatR;

public record GetProductHistoryQuery(Guid ProductId) : IRequest<IEnumerable<ProductResult>>;
