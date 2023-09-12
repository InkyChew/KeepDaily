using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class user_pwd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AppUser",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAENGhDb2kigcmR4mG0xdl+brNC8fE8oQfBtOWe44LMqJN/pEFX+AISxJHuc/Egt9fqA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AppUser",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAELI2MOeqVGWvOccZ0jExLatcpb349MyQhQiUeye8/Ei5IoIHoi/vXpvJhMmFAFW60Q==");
        }
    }
}
