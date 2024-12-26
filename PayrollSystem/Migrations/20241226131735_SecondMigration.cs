using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Stamp",
                table: "Roles",
                type: "bigint",
                maxLength: 10,
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stamp",
                table: "Roles");
        }
    }
}
