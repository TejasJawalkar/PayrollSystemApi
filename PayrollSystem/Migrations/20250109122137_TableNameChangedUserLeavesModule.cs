using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class TableNameChangedUserLeavesModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLeaves_Employee_EmployeeId",
                table: "UserLeaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLeaves",
                table: "UserLeaves");

            migrationBuilder.RenameTable(
                name: "UserLeaves",
                newName: "EmployeeLeaves");

            migrationBuilder.RenameIndex(
                name: "IX_UserLeaves_EmployeeId",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaves",
                table: "EmployeeLeaves",
                column: "LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_Employee_EmployeeId",
                table: "EmployeeLeaves",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_Employee_EmployeeId",
                table: "EmployeeLeaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaves",
                table: "EmployeeLeaves");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaves",
                newName: "UserLeaves");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "UserLeaves",
                newName: "IX_UserLeaves_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLeaves",
                table: "UserLeaves",
                column: "LeaveId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLeaves_Employee_EmployeeId",
                table: "UserLeaves",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
