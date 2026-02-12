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
                new Country { Id = 1, Name = "Estados Unidos" },
                new Country { Id = 1, Name = "Chile" },
                new Country { Id = 2, Name = "Argentina" }
            );
        });
    }
}
