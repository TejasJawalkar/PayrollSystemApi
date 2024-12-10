using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class addallmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartementName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    ExceptionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.ExceptionId);
                });

            migrationBuilder.CreateTable(
                name: "Orgnisations",
                columns: table => new
                {
                    OrgnisationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgnizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationPincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrgnisationDirectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationCeo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeoMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeoEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationGstNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationStartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgnisationEndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SystemRegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orgnisations", x => x.OrgnisationID);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    LogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BrowswerUsed = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgnisationID = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Orgnisations_OrgnisationID",
                        column: x => x.OrgnisationID,
                        principalTable: "Orgnisations",
                        principalColumn: "OrgnisationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyTimeSheet",
                columns: table => new
                {
                    TimeSheetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    TodayDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalHoursWorked = table.Column<int>(type: "int", nullable: false),
                    AttendanceFlag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyTimeSheet", x => x.TimeSheetId);
                    table.ForeignKey(
                        name: "FK_DailyTimeSheet_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSecurity",
                columns: table => new
                {
                    UserSecurityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    UserPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSecurity", x => x.UserSecurityId);
                    table.ForeignKey(
                        name: "FK_EmployeeSecurity_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    CTC = table.Column<double>(type: "float", nullable: false),
                    GrossPay = table.Column<double>(type: "float", nullable: false),
                    NetPay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLeaves",
                columns: table => new
                {
                    LeaveId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    AppliedReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RejectedReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoofDays = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLeaves", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_UserLeaves_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyTimeSheet_EmployeeId",
                table: "DailyTimeSheet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OrganisationEmail",
                table: "Employee",
                column: "OrganisationEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OrgnisationID",
                table: "Employee",
                column: "OrgnisationID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSecurity_EmployeeId",
                table: "EmployeeSecurity",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EmployeeId",
                table: "Payments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLeaves_EmployeeId",
                table: "UserLeaves",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyTimeSheet");

            migrationBuilder.DropTable(
                name: "EmployeeSecurity");

            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "UserLeaves");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Orgnisations");
        }
    }
}
