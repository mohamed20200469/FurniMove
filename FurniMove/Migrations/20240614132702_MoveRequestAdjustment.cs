using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class MoveRequestAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "MoveRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "ETA",
                table: "MoveRequests",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "EndAdress",
                table: "MoveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAdress",
                table: "MoveRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "ETA",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "EndAdress",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "StartAdress",
                table: "MoveRequests");
        }
    }
}
