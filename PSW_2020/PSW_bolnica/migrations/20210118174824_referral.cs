using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class referral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ReferralId",
                table: "user",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.AlterColumn<bool>(
                name: "isCanceled",
                table: "Appointment",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "ReferralId",
                table: "user");

            migrationBuilder.AlterColumn<bool>(
                name: "isCanceled",
                table: "Appointment",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
