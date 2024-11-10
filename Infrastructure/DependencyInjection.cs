namespace Infrastructure;

using Application.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbEngine, DbEngine>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<IProductRepository>(provider =>
            new ProductRepository(provider.GetRequiredService<IDbEngine>(), connectionString));
    }
}
