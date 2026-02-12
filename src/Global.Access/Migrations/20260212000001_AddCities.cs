using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Access.Migrations;

public partial class AddCities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Cities",
            schema: "dbo",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                CountryId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cities", x => x.Id);
                table.ForeignKey(
                    name: "FK_Cities_Countries_CountryId",
                    column: x => x.CountryId,
                    principalSchema: "dbo",
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Cities_CountryId",
            schema: "dbo",
            table: "Cities",
            column: "CountryId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Cities",
            schema: "dbo");
    }
}
