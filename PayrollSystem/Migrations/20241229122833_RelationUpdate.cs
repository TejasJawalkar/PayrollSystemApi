using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class RelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_RoleId",
                table: "Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoleId",
                table: "Employee",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_RoleId",
                table: "Employee");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_RoleId",
                table: "Employee",
                column: "RoleId",
                unique: true);
        }
    }
}
