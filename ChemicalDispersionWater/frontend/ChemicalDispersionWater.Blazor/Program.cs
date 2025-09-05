using ChemicalDispersionWater.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL EF Core DbContext with NetTopologySuite for spatial support
builder.Services.AddDbContext<ChemicalDispersionDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite()
    )
);

var app = builder.Build();

// Configure CORS for development
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Minimal API endpoints
app.MapGet("/", () => "Chemical Dispersion Water API is running!");

app.MapGet("/test", () => new { Status = "OK", Time = DateTime.Now });

// Ensure database is created
try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ChemicalDispersionDbContext>();
    context.Database.EnsureCreated();
    Console.WriteLine("Database created successfully");
}
catch (Exception ex)
{
    Console.WriteLine($"Database error: {ex.Message}");
}

app.Run();