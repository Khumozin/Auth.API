using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.API.Migrations
{
    public partial class AddPropsTouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateUpdated",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "User");
        }
    }
}
