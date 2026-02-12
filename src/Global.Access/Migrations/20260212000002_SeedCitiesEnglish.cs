using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Global.Access.Migrations;

public partial class SeedCitiesEnglish : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "dbo",
            table: "Countries",
            keyColumn: "Id",
            keyValue: 2,
            column: "Name",
            value: "United States");

        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Cities] ON;");
        migrationBuilder.InsertData(
            schema: "dbo",
            table: "Cities",
            columns: new[] { "Id", "CountryId", "Name" },
            values: new object[,]
            {
                { 1, 1, "Asuncion" },
                { 2, 2, "Washington" },
                { 3, 3, "Santiago" },
                { 4, 4, "Buenos Aires" }
            });
        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[Cities] OFF;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "dbo",
            table: "Cities",
            keyColumn: "Id",
            keyValues: new object[] { 1, 2, 3, 4 });

        migrationBuilder.UpdateData(
            schema: "dbo",
            table: "Countries",
            keyColumn: "Id",
            keyValue: 2,
            column: "Name",
            value: "Estados Unidos");
    }
}
