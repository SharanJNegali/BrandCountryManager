using Microsoft.EntityFrameworkCore;
using BrandCountryManager.Models.Entities;

namespace BrandCountryManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Country entity configuration
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IsoCode).IsRequired().HasMaxLength(3);
                entity.HasIndex(e => e.IsoCode).IsUnique();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.UpdatedDate).IsRequired();
            });

            // Brand entity configuration
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CountryId).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.UpdatedDate).IsRequired();

                // Foreign key relationship
                entity.HasOne(b => b.Country)
                      .WithMany(c => c.Brands)
                      .HasForeignKey(b => b.CountryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var countries = new[]
            {
                new Country
                {
                    Id = 1,
                    Name = "United States",
                    IsoCode = "USA",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Country
                {
                    Id = 2,
                    Name = "United Kingdom",
                    IsoCode = "GBR",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Country
                {
                    Id = 3,
                    Name = "Germany",
                    IsoCode = "DEU",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Country
                {
                    Id = 4,
                    Name = "Japan",
                    IsoCode = "JPN",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };

            var brands = new[]
            {
                new Brand
                {
                    Id = 1,
                    Name = "Apple",
                    Description = "Technology company known for iPhone, iPad, and Mac",
                    CountryId = 1,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 2,
                    Name = "Microsoft",
                    Description = "Software company known for Windows and Office",
                    CountryId = 1,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 3,
                    Name = "BMW",
                    Description = "Luxury automotive manufacturer",
                    CountryId = 3,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                },
                new Brand
                {
                    Id = 4,
                    Name = "Toyota",
                    Description = "Japanese automotive manufacturer",
                    CountryId = 4,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<Brand>().HasData(brands);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Country || x.Entity is Brand)
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is Country country)
                    {
                        country.CreatedDate = now;
                        country.UpdatedDate = now;
                    }
                    else if (entity.Entity is Brand brand)
                    {
                        brand.CreatedDate = now;
                        brand.UpdatedDate = now;
                    }
                }
                else if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity is Country country)
                    {
                        country.UpdatedDate = now;
                    }
                    else if (entity.Entity is Brand brand)
                    {
                        brand.UpdatedDate = now;
                    }
                }
            }
        }
    }
}