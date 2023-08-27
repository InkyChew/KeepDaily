using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class update_user_image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppUser",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgName",
                table: "AppUser",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgType",
                table: "AppUser",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEKyVHwz+VNL/+v4fEFZtVH/Y8KRuF2Zkh3scCOz9LtjjZXg2EB0KEfUGxSyrP6wDow==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "ImgName",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "ImgType",
                table: "AppUser");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEHAKdeCaobahtanlK2qe0Pcu6GOAdwkcTcr/ZBeyKozN+Okzqofk+sXcvwkgh/TRNQ==");
        }
    }
}
