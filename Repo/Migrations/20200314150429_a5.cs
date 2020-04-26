using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLines_MobilePhones_IMEI",
                table: "MobilePhoneWithLines");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLines_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLines");

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

            migrationBuilder.CreateTable(
                name: "CallingPlans",
                columns: table => new
                {
                    CallingPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Minutes = table.Column<int>(nullable: false),
                    Messages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallingPlans", x => x.CallingPlanId);
                });

            migrationBuilder.CreateTable(
                name: "DataPlans",
                columns: table => new
                {
                    DataPlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalData = table.Column<int>(nullable: false),
                    InternationalData = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPlans", x => x.DataPlanId);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhoneCalls",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    IMEI = table.Column<long>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Addressee = table.Column<int>(nullable: false),
                    MobilePhoneCallDuration = table.Column<int>(nullable: false),
                    MobilePhoneCallCost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneCalls", x => new { x.PhoneNumber, x.IMEI, x.DateTime });
                    table.ForeignKey(
                        name: "FK_MobilePhoneCalls_MobilePhones_IMEI",
                        column: x => x.IMEI,
                        principalTable: "MobilePhones",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobilePhoneCalls_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneCalls_IMEI",
                table: "MobilePhoneCalls",
                column: "IMEI");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLine_MobilePhones_IMEI",
                table: "MobilePhoneWithLine");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneWithLine_PhoneLines_PhoneNumber",
                table: "MobilePhoneWithLine");

            migrationBuilder.DropTable(
                name: "CallingPlans");

            migrationBuilder.DropTable(
                name: "DataPlans");

            migrationBuilder.DropTable(
                name: "MobilePhoneCalls");

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
    }
}
