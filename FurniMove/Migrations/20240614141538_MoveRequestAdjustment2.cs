using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class MoveRequestAdjustment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartAdress",
                table: "MoveRequests",
                newName: "StartAddress");

            migrationBuilder.RenameColumn(
                name: "EndAdress",
                table: "MoveRequests",
                newName: "EndAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartAddress",
                table: "MoveRequests",
                newName: "StartAdress");

            migrationBuilder.RenameColumn(
                name: "EndAddress",
                table: "MoveRequests",
                newName: "EndAdress");
        }
    }
}
