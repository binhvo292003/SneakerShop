using Microsoft.EntityFrameworkCore;
using SneakerShop.API.Extensions;
using SneakerShop.Application.Settings;
using SneakerShop.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApiServices();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    await context.Database.MigrateAsync();

    await DbInitializer.Initialize(app);

    logger.LogInformation("Database migration and initialization completed successfully");
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "An error occurred during database migration or initialization");
}

app.Run();