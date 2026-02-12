using Global.Access.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Global.Access.Migrations;

[DbContext(typeof(GlobalDbContext))]
partial class GlobalDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "9.0.4")
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity("Global.Manager.Entities.Country", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            b.HasKey("Id");

            b.ToTable("Countries", "dbo");

            b.HasData(
                new
                {
                    Id = 1,
                    Name = "Chile"
                },
                new
                {
                    Id = 2,
                    Name = "Argentina"
                });
        });
    }
}
