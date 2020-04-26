using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLine_MobilePhones_IMEI",
                table: "MobilePhoneWithLine");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLine_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneWithLine",
                table: "MobilePhoneWithLine");

            migrationBuilder.RenameTable(
                name: "MobilePhoneWithLine",
                newName: "MobilePhoneWithLines");

            migrationBuilder.RenameIndex(
                name: "IX_MobilePhoneWithLine_IMEI",
                table: "MobilePhoneWithLines",
                newName: "IX_MobilePhoneWithLines_IMEI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneWithLines",
                table: "MobilePhoneWithLines",
                columns: new[] { "PhoneNumber", "IMEI" });

            migrationBuilder.CreateTable(
                name: "PhoneLineEmployee",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLineEmployee", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_PhoneLineEmployee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneLineEmployee_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLineEmployee_EmployeeId",
                table: "PhoneLineEmployee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneWithLines_MobilePhones_IMEI",
                table: "MobilePhoneWithLines",
                column: "IMEI",
                principalTable: "MobilePhones",
                principalColumn: "IMEI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneWithLines_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLines",
                column: "PhoneNumber",
                principalTable: "PhoneLines",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLines_MobilePhones_IMEI",
                table: "MobilePhoneWithLines");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLines_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLines");

            migrationBuilder.DropTable(
                name: "PhoneLineEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneWithLines",
                table: "MobilePhoneWithLines");

            migrationBuilder.RenameTable(
                name: "MobilePhoneWithLines",
                newName: "MobilePhoneWithLine");

            migrationBuilder.RenameIndex(
                name: "IX_MobilePhoneWithLines_IMEI",
                table: "MobilePhoneWithLine",
                newName: "IX_MobilePhoneWithLine_IMEI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneWithLine",
                table: "MobilePhoneWithLine",
                columns: new[] { "PhoneNumber", "IMEI" });

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneWithLine_MobilePhones_IMEI",
                table: "MobilePhoneWithLine",
                column: "IMEI",
                principalTable: "MobilePhones",
                principalColumn: "IMEI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneWithLine_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLine",
                column: "PhoneNumber",
                principalTable: "PhoneLines",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
