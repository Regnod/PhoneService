using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandlinePhoneCall_Employees_EmployeeId",
                table: "LandlinePhoneCall");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LandlinePhoneCall",
                table: "LandlinePhoneCall");

            migrationBuilder.RenameTable(
                name: "LandlinePhoneCall",
                newName: "LandLinePhoneCalls");

            migrationBuilder.RenameIndex(
                name: "IX_LandlinePhoneCall_EmployeeId",
                table: "LandLinePhoneCalls",
                newName: "IX_LandLinePhoneCalls_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LandLinePhoneCalls",
                table: "LandLinePhoneCalls",
                columns: new[] { "Extension", "LandlinePhoneCallDateTime", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LandLinePhoneCalls_Employees_EmployeeId",
                table: "LandLinePhoneCalls",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandLinePhoneCalls_Employees_EmployeeId",
                table: "LandLinePhoneCalls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LandLinePhoneCalls",
                table: "LandLinePhoneCalls");

            migrationBuilder.RenameTable(
                name: "LandLinePhoneCalls",
                newName: "LandlinePhoneCall");

            migrationBuilder.RenameIndex(
                name: "IX_LandLinePhoneCalls_EmployeeId",
                table: "LandlinePhoneCall",
                newName: "IX_LandlinePhoneCall_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LandlinePhoneCall",
                table: "LandlinePhoneCall",
                columns: new[] { "Extension", "LandlinePhoneCallDateTime", "EmployeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LandlinePhoneCall_Employees_EmployeeId",
                table: "LandlinePhoneCall",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
