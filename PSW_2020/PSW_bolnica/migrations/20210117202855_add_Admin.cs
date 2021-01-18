using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class add_Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "admin",
               columns: table => new
               {
                   id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   name = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                   surname = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                   username = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                   password = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_admin", x => x.id);
               });
            //insert into tabels
            migrationBuilder.InsertData(
               table: "admin",
               columns: new[] { "id", "name", "surname", "username", "password"},
               values: new object[,]
               {
                    { 1, "ljubica", "prelic", "admin", "admin" },
               });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");
        }
    }
}
