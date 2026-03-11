using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Interfaces;
using ProductService.Middleware;
using ProductService.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();


// 1. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register the DbContext with the DI container
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(connectionString)); // Use UseSqlServer if using SQL Server

builder.Services.AddControllers();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
    options.InstanceName = "ProductService_"; // Prefix for keys in Redis
});

builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
