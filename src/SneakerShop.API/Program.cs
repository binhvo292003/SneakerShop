using Microsoft.EntityFrameworkCore;
using SneakerShop.Infrastructure.Data;
using SneakerShop.Domain.Repositories;
using SneakerShop.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add database context with connection string from configuration
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Apply migrations and seed database
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    // Apply pending migrations
    await context.Database.MigrateAsync();

    // Seed initial data
    await DbInitializer.Initialize(app);

    logger.LogInformation("Database migration and initialization completed successfully");
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "An error occurred during database migration or initialization");
}

app.Run();