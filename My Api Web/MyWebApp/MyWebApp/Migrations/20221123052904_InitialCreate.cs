using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(name: "user_name", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    userfullname = table.Column<string>(name: "user_fullname", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    useremail = table.Column<string>(name: "user_email", type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    userpassword = table.Column<string>(name: "user_password", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
