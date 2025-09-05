using ChemicalDispersionWater.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add CORS for Blazor WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:5002", "http://localhost:5003") // Adjust ports as needed
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add PostgreSQL EF Core DbContext with NetTopologySuite for spatial support
builder.Services.AddDbContext<ChemicalDispersionDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("BlazorPolicy");
app.UseAuthorization();

app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ChemicalDispersionDbContext>();
    context.Database.EnsureCreated();
}

app.Run();