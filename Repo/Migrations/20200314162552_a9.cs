using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LandlinePhoneCall",
                columns: table => new
                {
                    Extension = table.Column<int>(nullable: false),
                    LandlinePhoneCallDateTime = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    LandlinePhoneCallDuration = table.Column<int>(nullable: false),
                    LandlinePhoneCallAddressee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandlinePhoneCall", x => new { x.Extension, x.LandlinePhoneCallDateTime, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_LandlinePhoneCall_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LandlinePhoneCall_EmployeeId",
                table: "LandlinePhoneCall",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LandlinePhoneCall");
        }
    }
}
