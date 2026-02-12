using Global.Manager.Entities;
using Microsoft.EntityFrameworkCore;

namespace Global.Access.Data;

public class GlobalDbContext : DbContext
{
    public GlobalDbContext(DbContextOptions<GlobalDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasData(
                new Country { Id = 1, Name = "Paraguay" },
                new Country { Id = 2, Name = "United States" },
                new Country { Id = 3, Name = "Chile" },
                new Country { Id = 4, Name = "Argentina" }
            );
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("Cities", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.HasOne(e => e.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new City { Id = 1, Name = "Asuncion", CountryId = 1 },
                new City { Id = 2, Name = "Washington", CountryId = 2 },
                new City { Id = 3, Name = "Santiago", CountryId = 3 },
                new City { Id = 4, Name = "Buenos Aires", CountryId = 4 },
                new City { Id = 5, Name = "Encarnacion", CountryId = 1 },
                new City { Id = 6, Name = "Ciudad del Este", CountryId = 1 },
                new City { Id = 7, Name = "New York", CountryId = 2 },
                new City { Id = 8, Name = "Los Angeles", CountryId = 2 },
                new City { Id = 9, Name = "Valparaiso", CountryId = 3 },
                new City { Id = 10, Name = "Concepcion", CountryId = 3 },
                new City { Id = 11, Name = "Cordoba", CountryId = 4 },
                new City { Id = 12, Name = "Rosario", CountryId = 4 }
            );
        });
    }
}
