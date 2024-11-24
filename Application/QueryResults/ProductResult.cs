namespace Application.QueryResults;

public class ProductResult
{
    public Guid Id { get; set; }
    public string? Name { get; init; }
    public decimal Price { get; init; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}
