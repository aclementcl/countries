using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Access.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");

        migrationBuilder.CreateTable(
            name: "Countries",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Countries", x => x.Id);
            });

        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Countries] ON;");
        migrationBuilder.InsertData(
            schema: "dbo",
            table: "Countries",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Chile" },
                { 2, "Argentina" }
            });
        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Countries] OFF;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Countries",
            schema: "dbo");
    }
}
