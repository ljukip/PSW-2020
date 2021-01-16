using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class daeTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFrom",
                table: "Appointment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeTo",
                table: "Appointment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeFrom",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "DateTimeTo",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Appointment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
