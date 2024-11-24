namespace WebAPI.Controllers;

using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Product = Application.QueryResults.Product;

[Route("api/products"), ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] Product productDto, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received productDto: {productDto.Id}, {productDto.Name}, {productDto.Price}");

        var product = new Domain.Entities.Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Price = productDto.Price,
        };

        await this.productRepository.AddAsync(product, cancellationToken);

        var name = nameof(this.AddAsync);

        return this.Created();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(id, cancellationToken);

        if (product == null)
        {
            return this.NotFound();
        }

        await this.productRepository.DeleteAsync(id, cancellationToken);

        return this.NoContent();
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await this.productRepository.GetAllAsync(cancellationToken);

        var product = products.Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();

        return this.Ok(product);
    }

    // GET: api/products/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(id, cancellationToken);

        if (product == null)
        {
            return this.NotFound();
        }

        var productDto = new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            ValidFrom = product.ValidFrom,
            ValidTo = product.ValidTo,
        };

        return this.Ok(productDto);
    }

    // GET: api/products/{id}/history
    [HttpGet("{id}/history")]
    public async Task<ActionResult<IEnumerable<Product>>> GetHistoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var history = await this.productRepository.GetHistoryAsync(id, cancellationToken);

        var historyDtos = history.Select(p => new Product
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            ValidFrom = p.ValidFrom,
            ValidTo = p.ValidTo,
        }).ToList();

        return this.Ok(historyDtos);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] Product productDto, CancellationToken cancellationToken)
    {
        var existingProduct = await this.productRepository.GetAsync(id, cancellationToken);

        if (existingProduct == null)
        {
            return this.NotFound();
        }

        var product = new Domain.Entities.Product
        {
            Id = id,
            Name = productDto.Name,
            Price = productDto.Price,
        };

        await this.productRepository.UpdateAsync(product, cancellationToken);

        return this.NoContent();
    }
}
