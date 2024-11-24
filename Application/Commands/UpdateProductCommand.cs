namespace Application.Commands;

using MediatR;

public record UpdateProductCommand(Guid ProductId, string? Name, decimal Price) : IRequest<bool>;
