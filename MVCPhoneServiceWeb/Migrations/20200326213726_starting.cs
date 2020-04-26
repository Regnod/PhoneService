using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCPhoneServiceWeb.Migrations
{
    public partial class starting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(nullable: true),
                    CostCenter = table.Column<string>(nullable: true),
                    PersonalCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhones",
                columns: table => new
                {
                    IMEI = table.Column<int>(nullable: false),
                    Modelo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhones", x => x.IMEI);
                });

            migrationBuilder.CreateTable(
                name: "PhoneLines",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    PUK = table.Column<int>(nullable: false),
                    PIN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLines", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandLinePhoneCalls",
                columns: table => new
                {
                    Extension = table.Column<int>(nullable: false),
                    LandlinePhoneCallDateTime = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    LandlinePhoneCallCost = table.Column<int>(nullable: false),
                    LandlinePhoneCallDuration = table.Column<int>(nullable: false),
                    LandlinePhoneCallAddressee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandLinePhoneCalls", x => new { x.Extension, x.LandlinePhoneCallDateTime, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_LandLinePhoneCalls_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhoneEmployees",
                columns: table => new
                {
                    IMEI = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneEmployees", x => x.IMEI);
                    table.ForeignKey(
                        name: "FK_MobilePhoneEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobilePhoneEmployees_MobilePhones_IMEI",
                        column: x => x.IMEI,
                        principalTable: "MobilePhones",
                        principalColumn: "IMEI",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "MobilePhoneCalls",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    IMEI = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    MobilePhoneCallAddressee = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "MobilePhoneDataPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    DataPlanAssignmentDateTime = table.Column<DateTime>(nullable: false),
                    DataPlanId = table.Column<int>(nullable: false),
                    NationalDataUsage = table.Column<int>(nullable: false),
                    InternationalDataUsage = table.Column<int>(nullable: false),
                    MobilePhoneIMEI = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneDataPlanAssignments", x => new { x.PhoneNumber, x.DataPlanAssignmentDateTime, x.DataPlanId });
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

            migrationBuilder.CreateTable(
                name: "PhoneLineEmployees",
                columns: table => new
                {
                    PhoneNumber = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLineEmployees", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_PhoneLineEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneLineEmployees_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LandLinePhoneCalls_EmployeeId",
                table: "LandLinePhoneCalls",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneCallingPlanAssignments_CallingPlanId",
                table: "MobilePhoneCallingPlanAssignments",
                column: "CallingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneCalls_IMEI",
                table: "MobilePhoneCalls",
                column: "IMEI");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneDataPlanAssignments_DataPlanId",
                table: "MobilePhoneDataPlanAssignments",
                column: "DataPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneDataPlanAssignments_MobilePhoneIMEI",
                table: "MobilePhoneDataPlanAssignments",
                column: "MobilePhoneIMEI");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneEmployees_EmployeeId",
                table: "MobilePhoneEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLineEmployees_EmployeeId",
                table: "PhoneLineEmployees",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LandLinePhoneCalls");

            migrationBuilder.DropTable(
                name: "MobilePhoneCallingPlanAssignments");

            migrationBuilder.DropTable(
                name: "MobilePhoneCalls");

            migrationBuilder.DropTable(
                name: "MobilePhoneDataPlanAssignments");

            migrationBuilder.DropTable(
                name: "MobilePhoneEmployees");

            migrationBuilder.DropTable(
                name: "PhoneLineEmployees");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CallingPlans");

            migrationBuilder.DropTable(
                name: "DataPlans");

            migrationBuilder.DropTable(
                name: "MobilePhones");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "PhoneLines");
        }
    }
}
