using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Web_Core.Migrations
{
    public partial class addPErmissionMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_key = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    permission_description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "PivotRolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    PivotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PivotRolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PivotRolePermission_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permissions",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PivotRolePermission_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permissions_permission_key",
                table: "permissions",
                column: "permission_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PivotRolePermission_PermissionId",
                table: "PivotRolePermission",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PivotRolePermission");

            migrationBuilder.DropTable(
                name: "permissions");
        }
    }
}
