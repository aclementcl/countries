using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Access.Migrations;

public partial class SeedMoreCities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Cities] ON;");
        migrationBuilder.InsertData(
            schema: "dbo",
            table: "Cities",
            columns: new[] { "Id", "CountryId", "Name" },
            values: new object[,]
            {
                { 5, 1, "Encarnacion" },
                { 6, 1, "Ciudad del Este" },
                { 7, 2, "New York" },
                { 8, 2, "Los Angeles" },
                { 9, 3, "Valparaiso" },
                { 10, 3, "Concepcion" },
                { 11, 4, "Cordoba" },
                { 12, 4, "Rosario" }
            });
        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Cities] OFF;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "dbo",
            table: "Cities",
            keyColumn: "Id",
            keyValues: new object[] { 5, 6, 7, 8, 9, 10, 11, 12 });
    }
}
