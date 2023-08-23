using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class update_day_img : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Day");

            migrationBuilder.AddColumn<string>(
                name: "ImgName",
                table: "Day",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgType",
                table: "Day",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgName",
                table: "Day");

            migrationBuilder.DropColumn(
                name: "ImgType",
                table: "Day");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Day",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
