namespace WebAPI.Controllers;

using Application.Commands;
using Application.Queries;
using Application.QueryResults;
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
    public async Task<ActionResult> AddAsync([FromBody] AddProductCommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received AddProduct Command: {command.Id}, {command.Name}, {command.Price}");

        await this.mediator.Send(command, cancellationToken);

        return this.Created();
    }

    // DELETE: api/products/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(DeleteProductCommand deleteProduct, CancellationToken cancellationToken)
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
        var products = await this.mediator.Send(new GetAllProductsQuery(), cancellationToken);

        return this.Ok(products);
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

        return this.Ok(product);
    }

    // GET: api/products/{id}/history
    [HttpGet("{id}/history")]
    public async Task<ActionResult<IEnumerable<ProductResult>>> GetHistoryAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductHistoryQuery(id);
        var history = await this.mediator.Send(query, cancellationToken);

        return this.Ok(history);
    }

    // PUT: api/products/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] ProductResult product, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(id, product.Name, product.Price);

        var result = await this.mediator.Send(command, cancellationToken);

        if (!result)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }
}
