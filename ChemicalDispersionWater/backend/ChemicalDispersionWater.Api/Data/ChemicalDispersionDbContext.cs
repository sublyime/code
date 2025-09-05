using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using ChemicalDispersionWater.Domain.Models;

namespace ChemicalDispersionWater.Api.Data
{
    public class ChemicalDispersionDbContext : DbContext
    {
        public ChemicalDispersionDbContext(DbContextOptions<ChemicalDispersionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Spill> Spills { get; set; } = null!;
        public DbSet<Chemical> Chemicals { get; set; } = null!;
        public DbSet<WeatherData> Weather { get; set; } = null!;
        public DbSet<TideInfo> Tides { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map spatial types if needed
            modelBuilder.Entity<Spill>(entity =>
            {
                entity.Property(e => e.Location).HasColumnType("geometry");
            });

            // Additional model configuration as needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
