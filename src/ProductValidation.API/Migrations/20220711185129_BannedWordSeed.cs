using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductValidation.API.Migrations
{
    /// <inheritdoc />
    public partial class BannedWordSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BannedWords",
                columns: new[] { "Id", "Brand", "Value" },
                values: new object[,]
                {
                    { 1, "nike", "adidas" },
                    { 2, "nike", "under armour" },
                    { 3, "nike", "underarmour" },
                    { 4, "adidas", "nike" },
                    { 5, "adidas", "puma" },
                    { 6, "apple", "google" },
                    { 7, "apple", "microsoft" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "BannedWords",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
