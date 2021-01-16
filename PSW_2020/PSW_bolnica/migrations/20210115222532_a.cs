using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "user",
                newName: "isBlocked");

            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "user",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Speciality",
                table: "Referral",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Speciality",
                table: "Doctor",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isBlocked",
                table: "user",
                newName: "IsBlocked");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlocked",
                table: "user",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality",
                table: "Referral",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Speciality",
                table: "Doctor",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
