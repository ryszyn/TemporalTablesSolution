﻿namespace Domain.Entities;

public sealed class Product
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
