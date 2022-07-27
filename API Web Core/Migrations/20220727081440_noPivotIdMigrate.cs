using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Web_Core.Migrations
{
    public partial class noPivotIdMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PivotId",
                table: "PivotUserRole");

            migrationBuilder.DropColumn(
                name: "PivotId",
                table: "PivotRolePermission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PivotId",
                table: "PivotUserRole",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PivotId",
                table: "PivotRolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
