using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "MoveCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34980463-6c7b-42e7-b8bf-846a0b1a0831", null, "ServiceProvider", "SERVICEPROVIDER" },
                    { "3eb9518b-7a4c-4bc3-9cf3-df9bc09bbc53", null, "Customer", "CUSTOMER" },
                    { "409b5d3f-a440-4e62-9049-c7382a06d7d8", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34980463-6c7b-42e7-b8bf-846a0b1a0831");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3eb9518b-7a4c-4bc3-9cf3-df9bc09bbc53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "409b5d3f-a440-4e62-9049-c7382a06d7d8");

            migrationBuilder.AlterColumn<int>(
                name: "MoveCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
