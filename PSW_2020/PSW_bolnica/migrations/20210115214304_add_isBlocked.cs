using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class add_isBlocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
               name: "isBlocked",
               table: "user",
               type: "bit",
               nullable: true,
               defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "user");
        }
    }
}
