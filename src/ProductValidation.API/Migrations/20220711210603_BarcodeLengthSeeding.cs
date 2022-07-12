using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductValidation.API.Migrations
{
    /// <inheritdoc />
    public partial class BarcodeLengthSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "BarcodeLengths",
                newName: "Value");

            migrationBuilder.InsertData(
                table: "BarcodeLengths",
                columns: new[] { "Id", "Brand", "Value" },
                values: new object[,]
                {
                    { 1, "nike", 12 },
                    { 2, "adidas", 15 },
                    { 3, "apple", 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarcodeLengths",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BarcodeLengths",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BarcodeLengths",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "BarcodeLengths",
                newName: "Length");
        }
    }
}
