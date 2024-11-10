namespace Infrastructure.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Dapper;

internal sealed class ProductRepository : IProductRepository
{
    private const string DELETE_COMMAND =
        """
            DELETE
            FROM
                [dbo].[Product]
            WHERE
                [Id] = @Id;
        """;

    private const string GET_ALL_QUERY =
        """
            SELECT
                [Id],
                [Name],
                [Price],
                [ValidFrom],
                [ValidTo]
            FROM
                [dbo].[Product];
        """;

    private const string GET_BY_ID_QUERY =
        """
            SELECT
                [Id],
                [Name],
                [Price],
                [ValidFrom],
                [ValidTo]
            FROM
                [dbo].[Product]
            WHERE
                [Id] = @Id;
        """;

    private const string GET_HISTORY_QUERY =
        """
            SELECT
                [Id],
                [Name],
                [Price],
                [ValidFrom],
                [ValidTo]
            FROM
                [dbo].[Product]
            FOR SYSTEM_TIME ALL
            WHERE
                [Id] = @Id
            ORDER BY
                [ValidFrom] DESC;
        """;

    private const string INSERT_COMMAND =
        """
            INSERT INTO [dbo].[Product]
            (
                [Id],
                [Name],
                [Price]
            )
            VALUES
            (
                @Name,
                @Price
            );
        """;

    private const string UPDATE_COMMAND =
        """
            UPDATE [dbo].[Product]
            SET
                [Name] = @Name,
                [Price] = @Price
            WHERE
                [Id] = @Id;
        """;

    private readonly string connectionString;

    private readonly IDbEngine dbEngine;

    public ProductRepository(IDbEngine dbEngine, string connectionString)
    {
        this.dbEngine = dbEngine;
        this.connectionString = connectionString;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        var dbModel = new
        {
            product.Id,
            product.Name,
            product.Price,
        };

        await this.dbEngine.ExecuteAsync(this.connectionString, INSERT_COMMAND, dbModel, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id,
        };

        await this.dbEngine.ExecuteAsync(this.connectionString, DELETE_COMMAND, parameters, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await this.dbEngine.QueryAsync<Product>(this.connectionString, GET_ALL_QUERY, parameters: null, cancellationToken);

        return products;
    }

    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id,
        };

        var product = await this.dbEngine.FirstOrDefaultAsync<Product>(this.connectionString, GET_BY_ID_QUERY, parameters, cancellationToken);

        return product;
    }

    public async Task<IEnumerable<Product>> GetHistoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var parameters = new
        {
            Id = id,
        };

        var productHistory = await this.dbEngine.QueryAsync<Product>(this.connectionString, GET_HISTORY_QUERY, parameters, cancellationToken);

        return productHistory;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var dbModel = new
        {
            product.Id,
            product.Name,
            product.Price,
        };

        await this.dbEngine.ExecuteAsync(this.connectionString, UPDATE_COMMAND, dbModel, cancellationToken);
    }
}
