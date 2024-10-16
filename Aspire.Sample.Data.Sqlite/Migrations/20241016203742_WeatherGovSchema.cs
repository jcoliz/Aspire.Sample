using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aspire.Sample.Data.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class WeatherGovSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WeatherForecasts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "TemperatureC",
                table: "WeatherForecasts",
                newName: "TemperatureF");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemperatureF",
                table: "WeatherForecasts",
                newName: "TemperatureC");

            migrationBuilder.InsertData(
                table: "WeatherForecasts",
                columns: new[] { "Id", "Date", "Summary", "TemperatureC" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 7, 25), "Seeded", 101 },
                    { 2, new DateOnly(2024, 7, 24), "Seeded", 102 },
                    { 3, new DateOnly(2024, 7, 23), "Seeded", 103 }
                });
        }
    }
}
