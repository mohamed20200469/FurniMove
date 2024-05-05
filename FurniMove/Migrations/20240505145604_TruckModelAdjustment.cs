using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class TruckModelAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceProviderId",
                table: "Trucks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ServiceProviderId",
                table: "Trucks",
                column: "ServiceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_AspNetUsers_ServiceProviderId",
                table: "Trucks",
                column: "ServiceProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_AspNetUsers_ServiceProviderId",
                table: "Trucks");

            migrationBuilder.DropIndex(
                name: "IX_Trucks_ServiceProviderId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ServiceProviderId",
                table: "Trucks");
        }
    }
}
