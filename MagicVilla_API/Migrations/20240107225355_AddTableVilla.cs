using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AddTableVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Description", "ImagenUrl", "Name", "Occupants", "Rate", "Services", "SquareMeters" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 19, 53, 55, 334, DateTimeKind.Local).AddTicks(7022), new DateTime(2024, 1, 7, 19, 53, 55, 334, DateTimeKind.Local).AddTicks(7035), "Detail of the villa...", "", "Villa Real", 5, 200.0, "", 50 },
                    { 2, new DateTime(2024, 1, 7, 19, 53, 55, 334, DateTimeKind.Local).AddTicks(7037), new DateTime(2024, 1, 7, 19, 53, 55, 334, DateTimeKind.Local).AddTicks(7037), "Detail of the villa...", "", "Premium beach view", 4, 150.0, "", 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
