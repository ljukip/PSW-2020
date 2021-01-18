using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class dro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "referal",
               table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
