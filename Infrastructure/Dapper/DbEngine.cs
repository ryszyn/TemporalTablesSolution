namespace Infrastructure.Dapper;

using global::Dapper;
using Microsoft.Data.SqlClient;

public class DbEngine : IDbEngine
{
    public async Task<int> ExecuteAsync(string? connectionString, string command, object? parameters = null, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.ExecuteAsync(command, parameters);
    }

    public async Task<T?> FirstOrDefaultAsync<T>(string? connectionString, string commandText, object? parameters = null, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        var result = await this.QueryAsync<T>(connectionString, commandText, parameters, cancellationToken);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string? connectionString, string commandText, object? parameters = null, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        return await connection.QueryAsync<T>(commandText, parameters);
    }
}
