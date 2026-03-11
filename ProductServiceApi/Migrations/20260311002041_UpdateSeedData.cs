using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3f82057d-2b3a-449e-8930-466373e20601"),
                column: "CreatedAt",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7ed3a118-ef73-4560-8438-66275b634812"),
                column: "CreatedAt",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3f82057d-2b3a-449e-8930-466373e20601"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7ed3a118-ef73-4560-8438-66275b634812"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
