using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class TruckAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveOffers_AspNetUsers_serviceProviderId",
                table: "MoveOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveOffers_MoveRequests_moveRequestId",
                table: "MoveOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Locations_currentLocationId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "capacity",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "consumptionRate",
                table: "Trucks");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Trucks",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "plateNumber",
                table: "Trucks",
                newName: "PlateNumber");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Trucks",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "currentLocationId",
                table: "Trucks",
                newName: "CurrentLocationId");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "Trucks",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_Trucks_currentLocationId",
                table: "Trucks",
                newName: "IX_Trucks_CurrentLocationId");

            migrationBuilder.RenameColumn(
                name: "serviceProviderId",
                table: "MoveOffers",
                newName: "ServiceProviderId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "MoveOffers",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "moveRequestId",
                table: "MoveOffers",
                newName: "MoveRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveOffers_serviceProviderId",
                table: "MoveOffers",
                newName: "IX_MoveOffers_ServiceProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveOffers_moveRequestId",
                table: "MoveOffers",
                newName: "IX_MoveOffers_MoveRequestId");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "MoveOffers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOffers_AspNetUsers_ServiceProviderId",
                table: "MoveOffers",
                column: "ServiceProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOffers_MoveRequests_MoveRequestId",
                table: "MoveOffers",
                column: "MoveRequestId",
                principalTable: "MoveRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Locations_CurrentLocationId",
                table: "Trucks",
                column: "CurrentLocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveOffers_AspNetUsers_ServiceProviderId",
                table: "MoveOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveOffers_MoveRequests_MoveRequestId",
                table: "MoveOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_Locations_CurrentLocationId",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "MoveOffers");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Trucks",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "PlateNumber",
                table: "Trucks",
                newName: "plateNumber");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Trucks",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "CurrentLocationId",
                table: "Trucks",
                newName: "currentLocationId");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Trucks",
                newName: "brand");

            migrationBuilder.RenameIndex(
                name: "IX_Trucks_CurrentLocationId",
                table: "Trucks",
                newName: "IX_Trucks_currentLocationId");

            migrationBuilder.RenameColumn(
                name: "ServiceProviderId",
                table: "MoveOffers",
                newName: "serviceProviderId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "MoveOffers",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "MoveRequestId",
                table: "MoveOffers",
                newName: "moveRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveOffers_ServiceProviderId",
                table: "MoveOffers",
                newName: "IX_MoveOffers_serviceProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_MoveOffers_MoveRequestId",
                table: "MoveOffers",
                newName: "IX_MoveOffers_moveRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOffers_AspNetUsers_serviceProviderId",
                table: "MoveOffers",
                column: "serviceProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOffers_MoveRequests_moveRequestId",
                table: "MoveOffers",
                column: "moveRequestId",
                principalTable: "MoveRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_Locations_currentLocationId",
                table: "Trucks",
                column: "currentLocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
