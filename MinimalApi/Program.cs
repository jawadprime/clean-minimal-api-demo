using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Bootstrap;
using MinimalApi.Middlewares;
using MinimalApi.V1.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler(options => { });

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapProductEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}