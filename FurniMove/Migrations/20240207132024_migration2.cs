using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "endLocationId",
                table: "MoveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "endTime",
                table: "MoveRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numOfAppliances",
                table: "MoveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rating",
                table: "MoveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "startLocationId",
                table: "MoveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "MoveRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "MoveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "truckId",
                table: "MoveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    length = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    width = table.Column<double>(type: "float", nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    moveRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appliances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appliances_MoveRequests_moveRequestId",
                        column: x => x.moveRequestId,
                        principalTable: "MoveRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<int>(type: "int", nullable: false),
                    moveRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveOffers_MoveRequests_moveRequestId",
                        column: x => x.moveRequestId,
                        principalTable: "MoveRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    consumptionRate = table.Column<double>(type: "float", nullable: false),
                    currentLocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_Locations_currentLocationId",
                        column: x => x.currentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveRequests_endLocationId",
                table: "MoveRequests",
                column: "endLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveRequests_startLocationId",
                table: "MoveRequests",
                column: "startLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveRequests_truckId",
                table: "MoveRequests",
                column: "truckId");

            migrationBuilder.CreateIndex(
                name: "IX_Appliances_moveRequestId",
                table: "Appliances",
                column: "moveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOffers_moveRequestId",
                table: "MoveOffers",
                column: "moveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_currentLocationId",
                table: "Trucks",
                column: "currentLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveRequests_Locations_endLocationId",
                table: "MoveRequests",
                column: "endLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveRequests_Locations_startLocationId",
                table: "MoveRequests",
                column: "startLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveRequests_Trucks_truckId",
                table: "MoveRequests",
                column: "truckId",
                principalTable: "Trucks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveRequests_Locations_endLocationId",
                table: "MoveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveRequests_Locations_startLocationId",
                table: "MoveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveRequests_Trucks_truckId",
                table: "MoveRequests");

            migrationBuilder.DropTable(
                name: "Appliances");

            migrationBuilder.DropTable(
                name: "MoveOffers");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_MoveRequests_endLocationId",
                table: "MoveRequests");

            migrationBuilder.DropIndex(
                name: "IX_MoveRequests_startLocationId",
                table: "MoveRequests");

            migrationBuilder.DropIndex(
                name: "IX_MoveRequests_truckId",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "endLocationId",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "endTime",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "numOfAppliances",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "startLocationId",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "status",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "truckId",
                table: "MoveRequests");
        }
    }
}
