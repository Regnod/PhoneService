using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobilePhoneWithLine");

            migrationBuilder.DropColumn(
                name: "Addressee",
                table: "MobilePhoneCalls");

            migrationBuilder.AddColumn<int>(
                name: "MobilePhoneCallAddressee",
                table: "MobilePhoneCalls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MobilePhoneDataPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    DataPlanId = table.Column<int>(nullable: false),
                    NationalDataUsage = table.Column<int>(nullable: false),
                    InternationalDataUsage = table.Column<int>(nullable: false),
                    MobilePhoneIMEI = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneDataPlanAssignments", x => new { x.PhoneNumber, x.DateTime, x.DataPlanId });
                    table.ForeignKey(
                        name: "FK_MobilePhoneDataPlanAssignments_DataPlans_DataPlanId",
                        column: x => x.DataPlanId,
                        principalTable: "DataPlans",
                        principalColumn: "DataPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobilePhoneDataPlanAssignments_MobilePhones_MobilePhoneIMEI",
                        column: x => x.MobilePhoneIMEI,
                        principalTable: "MobilePhones",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MobilePhoneDataPlanAssignments_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneDataPlanAssignments_DataPlanId",
                table: "MobilePhoneDataPlanAssignments",
                column: "DataPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneDataPlanAssignments_MobilePhoneIMEI",
                table: "MobilePhoneDataPlanAssignments",
                column: "MobilePhoneIMEI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobilePhoneDataPlanAssignments");

            migrationBuilder.DropColumn(
                name: "MobilePhoneCallAddressee",
                table: "MobilePhoneCalls");

            migrationBuilder.AddColumn<int>(
                name: "Addressee",
                table: "MobilePhoneCalls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MobilePhoneWithLine",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    IMEI = table.Column<long>(type: "long", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneWithLine", x => new { x.PhoneNumber, x.IMEI });
                    table.ForeignKey(
                        name: "FK_MobilePhoneWithLine_MobilePhones_IMEI",
                        column: x => x.IMEI,
                        principalTable: "MobilePhones",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobilePhoneWithLine_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneWithLine_IMEI",
                table: "MobilePhoneWithLine",
                column: "IMEI");
        }
    }
}
