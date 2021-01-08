using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class Migration_create_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //creating tabels
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                    surname = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                    username = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    password = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    role = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(11)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phoneNumber = table.Column<bool>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });
            //insert into tabels
            migrationBuilder.InsertData(
               table: "user",
               columns: new[] { "id", "name", "surname", "username", "password", "role", "gender", "address", "phoneNumber" },
               values: new object[,]
               {
                    { 1, "ljubica", "prelic", "admin", "admin","ADMIN", "FEMAIL","bulevar 214","+381638877969" },
                    { 2, "pera", "peric", "user", "user","USER", "OTHER","bulevar 211","+381638877966" },
               });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
