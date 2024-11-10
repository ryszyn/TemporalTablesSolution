namespace Infrastructure;

using Application.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDbEngine, DbEngine>();

        services.AddSingleton<IProductRepository, ProductRepository>();
    }
}
