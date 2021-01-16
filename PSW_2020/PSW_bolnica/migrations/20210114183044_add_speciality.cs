using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class add_speciality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
               name: "speciality",
               table: "referral",
               type: "nvarchar(46)", maxLength: 46,
               nullable: false,
               defaultValue: false);

            migrationBuilder.AddColumn<bool>(
               name: "speciality",
               table: "doctor",
               type: "nvarchar(46)", maxLength: 46,
               nullable: true,
               defaultValue: false);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "speciality",
                table: "referral");
            migrationBuilder.DropColumn(
                name: "speciality",
                table: "doctor");

        }
    }
}
