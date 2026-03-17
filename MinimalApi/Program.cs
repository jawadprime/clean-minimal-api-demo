using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Bootstrap;
using MinimalApi.V1.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// Migrate the datbase before starting the app
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapProductEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
