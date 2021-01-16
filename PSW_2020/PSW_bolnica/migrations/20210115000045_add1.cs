using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class add1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
              name: "doctorId",
              table: "user",
              type: "nvarchar(46)", maxLength: 46,
              nullable: true);

            migrationBuilder.AddColumn<bool>(
               name: "referal",
               table: "user",
               type: "nvarchar(46)", maxLength: 46,
               nullable: true);

        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctorId",
                table: "user");
            migrationBuilder.DropColumn(
                name: "referral",
                table: "user");

        }
    }
}
