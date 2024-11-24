namespace Infrastructure;

using System.Reflection;
using Domain.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbEngine, DbEngine>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<IProductRepository>(provider =>
            new ProductRepository(provider.GetRequiredService<IDbEngine>(), connectionString));
    }
}
