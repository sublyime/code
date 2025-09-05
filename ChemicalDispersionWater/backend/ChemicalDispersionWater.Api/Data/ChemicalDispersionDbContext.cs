using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using ChemicalDispersionWater.SharedModels;

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

            // Seed initial data with static timestamps
            modelBuilder.Entity<Chemical>().HasData(
                new Chemical { Id = 1, Name = "Oil", Density = 0.85 },
                new Chemical { Id = 2, Name = "Acid", Density = 1.2 }
            );

            modelBuilder.Entity<Spill>().HasData(
                new Spill { Id = 1, ChemicalId = 1, Volume = 1000, Location = new Point(-122.4194, 37.7749) { SRID = 4326 }, Timestamp = new DateTime(2025, 9, 5, 12, 0, 0, DateTimeKind.Utc) },
                new Spill { Id = 2, ChemicalId = 2, Volume = 500, Location = new Point(-122.5, 37.8) { SRID = 4326 }, Timestamp = new DateTime(2025, 9, 5, 11, 0, 0, DateTimeKind.Utc) }
            );

            // Add seeding for WeatherData and TideInfo with static values if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}
