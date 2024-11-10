using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
