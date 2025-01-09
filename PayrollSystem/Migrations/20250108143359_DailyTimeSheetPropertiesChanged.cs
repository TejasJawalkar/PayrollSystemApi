using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class DailyTimeSheetPropertiesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TodayDate",
                table: "DailyTimeSheet",
                newName: "LoginDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "LogOutDate",
                table: "DailyTimeSheet",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogOutDate",
                table: "DailyTimeSheet");

            migrationBuilder.RenameColumn(
                name: "LoginDate",
                table: "DailyTimeSheet",
                newName: "TodayDate");
        }
    }
}
