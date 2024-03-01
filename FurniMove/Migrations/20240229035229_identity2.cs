using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class identity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09be18d9-8e73-474e-a07a-732f6b6846b6", null, "ServiceProvider", "SERVICEPROVIDER" },
                    { "31d6c577-5382-4b1c-a6e5-01dbbd7f172d", null, "Customer", "CUSTOMER" },
                    { "7a4231f1-33d3-4470-8d43-724835dac43e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09be18d9-8e73-474e-a07a-732f6b6846b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31d6c577-5382-4b1c-a6e5-01dbbd7f172d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a4231f1-33d3-4470-8d43-724835dac43e");
        }
    }
}
