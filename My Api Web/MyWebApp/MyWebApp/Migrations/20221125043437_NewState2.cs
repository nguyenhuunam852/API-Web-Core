using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewState2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "states",
               columns: table => new
               {
                   stateid = table.Column<int>(name: "state_id", type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   statepage = table.Column<int>(name: "state_page", type: "int", nullable: false, defaultValue: 1),
                   statefilter = table.Column<string>(name: "state_filter", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "*"),
                   UserId = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_states", x => x.stateid);
                   table.ForeignKey(
                       name: "FK_states_users_state_id",
                       column: x => x.UserId,
                       principalTable: "users",
                       principalColumn: "user_id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.RenameColumn(
                  name: "UserId",
                  table: "states",
                  newName: "user_state_id");

            migrationBuilder.AlterColumn<string>(
                name: "state_filter",
                table: "states",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldDefaultValue: "*");

            migrationBuilder.AlterColumn<string>(
                name: "state_filter",
                table: "states",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
