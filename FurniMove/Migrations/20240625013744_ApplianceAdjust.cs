using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurniMove.Migrations
{
    /// <inheritdoc />
    public partial class ApplianceAdjust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "height",
                table: "Appliances");

            migrationBuilder.DropColumn(
                name: "length",
                table: "Appliances");

            migrationBuilder.DropColumn(
                name: "width",
                table: "Appliances");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Appliances",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Appliances");

            migrationBuilder.AddColumn<double>(
                name: "height",
                table: "Appliances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "length",
                table: "Appliances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "width",
                table: "Appliances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
