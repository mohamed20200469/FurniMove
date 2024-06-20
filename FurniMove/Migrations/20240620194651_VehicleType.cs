using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class VehicleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleType",
                table: "MoveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "MoveRequests");
        }
    }
}
