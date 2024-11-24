namespace Application.Commands;

using System.Text.Json.Serialization;
using MediatR;

public sealed record DeleteProductCommand : IRequest
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
}
