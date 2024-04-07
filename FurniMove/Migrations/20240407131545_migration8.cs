using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customerId",
                table: "MoveRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serviceProviderId",
                table: "MoveRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serviceProviderId",
                table: "MoveOffers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveRequests_customerId",
                table: "MoveRequests",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveRequests_serviceProviderId",
                table: "MoveRequests",
                column: "serviceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOffers_serviceProviderId",
                table: "MoveOffers",
                column: "serviceProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOffers_AspNetUsers_serviceProviderId",
                table: "MoveOffers",
                column: "serviceProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveRequests_AspNetUsers_customerId",
                table: "MoveRequests",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveRequests_AspNetUsers_serviceProviderId",
                table: "MoveRequests",
                column: "serviceProviderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveOffers_AspNetUsers_serviceProviderId",
                table: "MoveOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveRequests_AspNetUsers_customerId",
                table: "MoveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveRequests_AspNetUsers_serviceProviderId",
                table: "MoveRequests");

            migrationBuilder.DropIndex(
                name: "IX_MoveRequests_customerId",
                table: "MoveRequests");

            migrationBuilder.DropIndex(
                name: "IX_MoveRequests_serviceProviderId",
                table: "MoveRequests");

            migrationBuilder.DropIndex(
                name: "IX_MoveOffers_serviceProviderId",
                table: "MoveOffers");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "serviceProviderId",
                table: "MoveRequests");

            migrationBuilder.DropColumn(
                name: "serviceProviderId",
                table: "MoveOffers");
        }
    }
}
