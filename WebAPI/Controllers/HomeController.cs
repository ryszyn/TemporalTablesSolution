namespace WebAPI.Controllers;

using Application.Commands;
using Application.QueryResults;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/products"), ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IProductRepository productRepository;

    public ProductController(IMediator mediator, IProductRepository productRepository)
    {
        this.mediator = mediator;
        this.productRepository = productRepository;
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult> AddAsync([FromBody] AddProduct command, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received AddProduct Command: {command.Id}, {command.Name}, {command.Price}");

        //TODO: warto sprawdzic, czy taki produkt juz istnieje... jeśli tak - rzucić wyjątkiem

        await this.mediator.Send(command, cancellationToken);

        return this.Created();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(DeleteProduct deleteProduct, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetAsync(deleteProduct.Id, cancellationToken);

        if (product == null)
        {
            return this.NotFound();
        }

        await this.productRepository.DeleteAsync(deleteProduct.Id, cancellationToken);

        return this.NoContent();
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResult>>> GetAllAsync(CancellationToken cancellationToken)
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
    public async Task<ActionResult<ProductResult>> GetAsync(Guid id, CancellationToken cancellationToken)
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
    public async Task<ActionResult<IEnumerable<ProductResult>>> GetHistoryAsync(Guid id, CancellationToken cancellationToken)
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
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ProductResult productDto, CancellationToken cancellationToken)
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
