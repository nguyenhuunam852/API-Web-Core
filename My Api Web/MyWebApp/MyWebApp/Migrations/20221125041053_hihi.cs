using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class hihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_states_users_state_id",
                table: "states");

            migrationBuilder.AlterColumn<int>(
                name: "state_id",
                table: "states",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_states_user_state_id",
                table: "states",
                column: "user_state_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_states_users_user_state_id",
                table: "states",
                column: "user_state_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_states_users_user_state_id",
                table: "states");

            migrationBuilder.DropIndex(
                name: "IX_states_user_state_id",
                table: "states");

            migrationBuilder.AlterColumn<int>(
                name: "state_id",
                table: "states",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_states_users_state_id",
                table: "states",
                column: "state_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
