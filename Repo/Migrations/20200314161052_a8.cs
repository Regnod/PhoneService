using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneEmployee_Employees_EmployeeId",
                table: "MobilePhoneEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneEmployee_MobilePhones_IMEI",
                table: "MobilePhoneEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneLineEmployee_Employees_EmployeeId",
                table: "PhoneLineEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneLineEmployee_PhoneLines_PhoneNumber",
                table: "PhoneLineEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneDataPlanAssignments",
                table: "MobilePhoneDataPlanAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneLineEmployee",
                table: "PhoneLineEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneEmployee",
                table: "MobilePhoneEmployee");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "MobilePhoneDataPlanAssignments");

            migrationBuilder.RenameTable(
                name: "PhoneLineEmployee",
                newName: "PhoneLineEmployees");

            migrationBuilder.RenameTable(
                name: "MobilePhoneEmployee",
                newName: "MobilePhoneEmployees");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneLineEmployee_EmployeeId",
                table: "PhoneLineEmployees",
                newName: "IX_PhoneLineEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_MobilePhoneEmployee_EmployeeId",
                table: "MobilePhoneEmployees",
                newName: "IX_MobilePhoneEmployees_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPlanAssignmentDateTime",
                table: "MobilePhoneDataPlanAssignments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneDataPlanAssignments",
                table: "MobilePhoneDataPlanAssignments",
                columns: new[] { "PhoneNumber", "DataPlanAssignmentDateTime", "DataPlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneLineEmployees",
                table: "PhoneLineEmployees",
                column: "PhoneNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneEmployees",
                table: "MobilePhoneEmployees",
                column: "IMEI");

            migrationBuilder.CreateTable(
                name: "MobilePhoneCallingPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    CallingPlanAssignmentDateTime = table.Column<DateTime>(nullable: false),
                    CallingPlanId = table.Column<int>(nullable: false),
                    MinutesConsumed = table.Column<int>(nullable: false),
                    MessagesSent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneCallingPlanAssignments", x => new { x.PhoneNumber, x.CallingPlanAssignmentDateTime, x.CallingPlanId });
                    table.ForeignKey(
                        name: "FK_MobilePhoneCallingPlanAssignments_CallingPlans_CallingPlanId",
                        column: x => x.CallingPlanId,
                        principalTable: "CallingPlans",
                        principalColumn: "CallingPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobilePhoneCallingPlanAssignments_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneCallingPlanAssignments_CallingPlanId",
                table: "MobilePhoneCallingPlanAssignments",
                column: "CallingPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneEmployees_Employees_EmployeeId",
                table: "MobilePhoneEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneEmployees_MobilePhones_IMEI",
                table: "MobilePhoneEmployees",
                column: "IMEI",
                principalTable: "MobilePhones",
                principalColumn: "IMEI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneLineEmployees_Employees_EmployeeId",
                table: "PhoneLineEmployees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneLineEmployees_PhoneLines_PhoneNumber",
                table: "PhoneLineEmployees",
                column: "PhoneNumber",
                principalTable: "PhoneLines",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneEmployees_Employees_EmployeeId",
                table: "MobilePhoneEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_MobilePhoneEmployees_MobilePhones_IMEI",
                table: "MobilePhoneEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneLineEmployees_Employees_EmployeeId",
                table: "PhoneLineEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneLineEmployees_PhoneLines_PhoneNumber",
                table: "PhoneLineEmployees");

            migrationBuilder.DropTable(
                name: "MobilePhoneCallingPlanAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneDataPlanAssignments",
                table: "MobilePhoneDataPlanAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneLineEmployees",
                table: "PhoneLineEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MobilePhoneEmployees",
                table: "MobilePhoneEmployees");

            migrationBuilder.DropColumn(
                name: "DataPlanAssignmentDateTime",
                table: "MobilePhoneDataPlanAssignments");

            migrationBuilder.RenameTable(
                name: "PhoneLineEmployees",
                newName: "PhoneLineEmployee");

            migrationBuilder.RenameTable(
                name: "MobilePhoneEmployees",
                newName: "MobilePhoneEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneLineEmployees_EmployeeId",
                table: "PhoneLineEmployee",
                newName: "IX_PhoneLineEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_MobilePhoneEmployees_EmployeeId",
                table: "MobilePhoneEmployee",
                newName: "IX_MobilePhoneEmployee_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "MobilePhoneDataPlanAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneDataPlanAssignments",
                table: "MobilePhoneDataPlanAssignments",
                columns: new[] { "PhoneNumber", "DateTime", "DataPlanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneLineEmployee",
                table: "PhoneLineEmployee",
                column: "PhoneNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MobilePhoneEmployee",
                table: "MobilePhoneEmployee",
                column: "IMEI");

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneEmployee_Employees_EmployeeId",
                table: "MobilePhoneEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MobilePhoneEmployee_MobilePhones_IMEI",
                table: "MobilePhoneEmployee",
                column: "IMEI",
                principalTable: "MobilePhones",
                principalColumn: "IMEI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneLineEmployee_Employees_EmployeeId",
                table: "PhoneLineEmployee",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneLineEmployee_PhoneLines_PhoneNumber",
                table: "PhoneLineEmployee",
                column: "PhoneNumber",
                principalTable: "PhoneLines",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
