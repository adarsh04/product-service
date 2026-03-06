using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductService.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Price", "Summary", "Title" },
                values: new object[,]
                {
                    { new Guid("3f82057d-2b3a-449e-8930-466373e20601"), new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 1249.50m, "Drop-proof tablet for real-time inventory tracking.", "Rugged Tablet X1" },
                    { new Guid("7ed3a118-ef73-4560-8438-66275b634812"), new DateTime(2026, 3, 2, 0, 0, 0, 0, DateTimeKind.Utc), 899.99m, "High-speed laser scanner for physical environments.", "Industrial Scanner Pro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
