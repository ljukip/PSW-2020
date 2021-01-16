using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PSW_bolnica.Migrations
{
    public partial class create_Doctor_Appointment_Referral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "doctor",
               columns: table => new
               {
                   id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                   name = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                   surname = table.Column<string>(type: "nvarchar(46)", maxLength: 46, nullable: true),
                   specialist = table.Column<bool>(type: "bit", nullable: true),
                   isDeleted = table.Column<bool>(type: "bit", nullable: false),
                   workingDays = table.Column<string>(type: "nvarchar(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_doctor", x => x.id);
               });

            migrationBuilder.InsertData(
               table: "doctor",
               columns: new[] { "id", "name", "surname", "specialist", "isDeleted", "workingDays" },
               values: new object[,]
               {
                    { 1, "Pera", "Peric", true, false, "2021-01-14 9 PM" },
                    { 2, "Mika", "Mikic",false,false, "2021-01-14 9 PM" },
                    { 3, "Zika", "Zikic", true,true, "2021-01-14 9 PM" },
                    { 4, "Jova", "Jovic", true,false, "2021-01-14 9 PM" }
               });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorId = table.Column<int>(type: "int", nullable: false),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.id);

                    table.ForeignKey(
                        name: "FK_appointment_user_PatientId",
                        column: x => x.patientId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);


                    table.ForeignKey(
                        name: "FK_appointment_doctor_DoctorId",
                        column: x => x.doctorId,
                        principalTable: "doctor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
              
                });

            migrationBuilder.CreateIndex(
               name: "IX_appointment_doctorId",
               table: "appointment",
               column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_patientId",
                table: "appointment",
                column: "patientId");

            migrationBuilder.CreateTable(
                name: "referral",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specialistId = table.Column<int>(type: "int", nullable: false),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral", x => x.id);
                    table.ForeignKey(
                        name: "FK_referral_doctor_specialistId",
                        column: x => x.specialistId,
                        principalTable: "doctor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_referral_user_patientId",
                        column: x => x.patientId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
               name: "IX_referral_patientId",
               table: "referral",
               column: "patientId",
               unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_referral_specialistId",
                table: "referral",
                column: "specialistId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doctor");
            migrationBuilder.DropTable(
                name: "appointment");
            migrationBuilder.DropTable(
                name: "referral");
        }
    }
}
