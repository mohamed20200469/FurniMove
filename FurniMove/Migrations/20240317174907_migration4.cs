using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MoveCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6adec7c6-cc4e-4960-88f7-86e500205797", null, "Admin", "ADMIN" },
                    { "c4452cea-c95e-4122-966a-995e854fbb4e", null, "Customer", "CUSTOMER" },
                    { "f61141fe-6cb0-47d7-9c16-d0b3fc49fe6c", null, "ServiceProvider", "SERVICEPROVIDER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6adec7c6-cc4e-4960-88f7-86e500205797");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4452cea-c95e-4122-966a-995e854fbb4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f61141fe-6cb0-47d7-9c16-d0b3fc49fe6c");

            migrationBuilder.DropColumn(
                name: "MoveCounter",
                table: "AspNetUsers");

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
    }
}
