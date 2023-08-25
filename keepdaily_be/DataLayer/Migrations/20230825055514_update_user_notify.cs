using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class update_user_notify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "Notify",
                table: "AppUser",
                newName: "UserLevel");

            migrationBuilder.AddColumn<bool>(
                name: "EmailNotify",
                table: "AppUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LineNotify",
                table: "AppUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmailNotify", "LineNotify", "Password" },
                values: new object[] { true, true, "AQAAAAEAACcQAAAAEPboLmdMsd7JGPgbh2bFnZuUE6xAP4rH9t1x8/WVCAYIu83jGf/aMf6JDLUOyjs48g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailNotify",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "LineNotify",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "UserLevel",
                table: "AppUser",
                newName: "Notify");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AppUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Level", "Password" },
                values: new object[] { 1, "AQAAAAEAACcQAAAAEC/2HbbS1a3CX3i+86ReHA1pu6p3Gq+3MJr4uetpbPgg53zkqsz9HSnicSWkDLrkcQ==" });
        }
    }
}
