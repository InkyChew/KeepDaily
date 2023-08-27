using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class update_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LineNotify", "Password" },
                values: new object[] { false, "AQAAAAEAACcQAAAAEA/0dFd1il2Jlz/ephs8a8lopVVfu/i5SJ1t9jGs6MrUPc4keDIV4qXw+AggrMS1EA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_CategoryId",
                table: "Plan",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetDefault);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Category_CategoryId",
                table: "Plan");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Plan_CategoryId",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Plan");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LineNotify", "Password" },
                values: new object[] { true, "AQAAAAEAACcQAAAAEPboLmdMsd7JGPgbh2bFnZuUE6xAP4rH9t1x8/WVCAYIu83jGf/aMf6JDLUOyjs48g==" });
        }
    }
}
