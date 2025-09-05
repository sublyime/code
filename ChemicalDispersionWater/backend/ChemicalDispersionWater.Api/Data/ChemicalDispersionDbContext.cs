using Microsoft.EntityFrameworkCore;
using ChemicalDispersionWater.SharedModels;

namespace ChemicalDispersionWater.Api.Data
{
    public class ChemicalDispersionDbContext : DbContext
    {
        public ChemicalDispersionDbContext(DbContextOptions<ChemicalDispersionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chemical> Chemicals { get; set; } = null!;
        public DbSet<Spill> Spills { get; set; } = null!;
        public DbSet<WeatherData> WeatherData { get; set; } = null!;
        public DbSet<TideInfo> TideInfos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Chemical entity
            modelBuilder.Entity<Chemical>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Density).IsRequired();
                entity.Property(e => e.MolecularWeight).IsRequired();
                entity.Property(e => e.BoilingPointC).IsRequired();
                entity.Property(e => e.SolubilityMgL).IsRequired();
            });

            // Configure Spill entity
            modelBuilder.Entity<Spill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Volume).IsRequired();
                entity.Property(e => e.Location).HasColumnType("geography (point)");
                entity.Property(e => e.Timestamp).IsRequired();

                entity.HasOne(e => e.Chemical)
                    .WithMany()
                    .HasForeignKey(e => e.ChemicalId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure WeatherData entity
            modelBuilder.Entity<WeatherData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.TemperatureC).IsRequired();
                entity.Property(e => e.Humidity).IsRequired();
                entity.Property(e => e.WindSpeed).IsRequired();
                entity.Property(e => e.WindDirection).IsRequired();
            });

            // Configure TideInfo entity
            modelBuilder.Entity<TideInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.TideHeight).IsRequired();
                entity.Property(e => e.TideType).HasMaxLength(50);
            });
        }
    }
}