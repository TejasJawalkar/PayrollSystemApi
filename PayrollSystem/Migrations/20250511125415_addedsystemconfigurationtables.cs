using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollSystem.Migrations
{
    public partial class addedsystemconfigurationtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutingNavigationMain",
                columns: table => new
                {
                    MainRouteId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizedUsers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingNavigationMain", x => x.MainRouteId);
                });

            migrationBuilder.CreateTable(
                name: "RoutingNavigationChild",
                columns: table => new
                {
                    ChildRouteID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainRouteId = table.Column<long>(type: "bigint", nullable: false),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingNavigationChild", x => x.ChildRouteID);
                    table.ForeignKey(
                        name: "FK_RoutingNavigationChild_RoutingNavigationMain_MainRouteId",
                        column: x => x.MainRouteId,
                        principalTable: "RoutingNavigationMain",
                        principalColumn: "MainRouteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutingNavigationChild_MainRouteId",
                table: "RoutingNavigationChild",
                column: "MainRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutingNavigationChild");

            migrationBuilder.DropTable(
                name: "RoutingNavigationMain");
        }
    }
}
