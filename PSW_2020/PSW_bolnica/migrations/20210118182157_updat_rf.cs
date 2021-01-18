using Microsoft.EntityFrameworkCore.Migrations;

namespace PSW_bolnica.Migrations
{
    public partial class updat_rf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "referral",
               columns: table => new
               {
                   id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   specialistId = table.Column<int>(type: "int", nullable: false),
                   idOfPatient = table.Column<int>(type: "int", nullable: false),
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
                       name: "FK_referral_user_idOfPatient",
                       column: x => x.idOfPatient,
                       principalTable: "user",
                       principalColumn: "id",
                       onDelete: ReferentialAction.Cascade);
               });
        }

    

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "referral");

        }
    }
}
