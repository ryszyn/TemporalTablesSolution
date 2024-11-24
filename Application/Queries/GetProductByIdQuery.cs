namespace Application.Queries;

using Application.Models;
using MediatR;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
