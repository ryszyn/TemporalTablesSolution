namespace Infrastructure.Dapper;

public interface IDbEngine
{
    Task<int> ExecuteAsync(string? connectionString, string command, object? parameters = null, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync<T>(string? connectionString, string commandText, object? parameters = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> QueryAsync<T>(string? connectionString, string commandText, object? parameters = null, CancellationToken cancellationToken = default);
}
