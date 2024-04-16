using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class editUserImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserImg",
                table: "AspNetUsers",
                newName: "UserImgURL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserImgURL",
                table: "AspNetUsers",
                newName: "UserImg");
        }
    }
}
