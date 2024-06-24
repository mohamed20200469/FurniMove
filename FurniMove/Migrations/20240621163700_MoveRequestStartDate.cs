using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class MoveRequestStartDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "MoveRequests");

            migrationBuilder.AddColumn<DateOnly>(
                name: "startDate",
                table: "MoveRequests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "startDate",
                table: "MoveRequests");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Trucks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "MoveRequests",
                type: "datetime2",
                nullable: true);
        }
    }
}
