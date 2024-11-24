namespace Application.Queries;

using Application.QueryResults;
using MediatR;
using System.Collections.Generic;

public record GetProductHistoryQuery(Guid ProductId) : IRequest<IEnumerable<ProductResult>>;
