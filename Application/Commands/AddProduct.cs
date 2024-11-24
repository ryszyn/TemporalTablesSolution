namespace Application.Commands;

using System.Text.Json.Serialization;
using MediatR;

public sealed record AddProduct : IRequest
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("price")]
    public required decimal Price { get; init; }
}
