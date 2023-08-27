using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class update_category2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Plan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEHAKdeCaobahtanlK2qe0Pcu6GOAdwkcTcr/ZBeyKozN+Okzqofk+sXcvwkgh/TRNQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEA/0dFd1il2Jlz/ephs8a8lopVVfu/i5SJ1t9jGs6MrUPc4keDIV4qXw+AggrMS1EA==");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
