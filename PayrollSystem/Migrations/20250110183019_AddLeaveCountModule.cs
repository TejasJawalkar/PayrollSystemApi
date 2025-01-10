using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class AddLeaveCountModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EmployeeLeavesAssignedId",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "YearlyLeavesAssigned",
                columns: table => new
                {
                    EmployeeLeavesAssignedId = table.Column<long>(type: "bigint", nullable: false),
                    TotalLeaves = table.Column<double>(type: "float", nullable: false),
                    ForYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyLeavesAssigned", x => x.EmployeeLeavesAssignedId);
                    table.ForeignKey(
                        name: "FK_YearlyLeavesAssigned_Employee_EmployeeLeavesAssignedId",
                        column: x => x.EmployeeLeavesAssignedId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearlyLeavesAssigned");

            migrationBuilder.DropColumn(
                name: "EmployeeLeavesAssignedId",
                table: "Employee");
        }
    }
}
