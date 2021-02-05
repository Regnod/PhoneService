using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class start : Migration
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
                name: "Bills",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "CallingPlans",
                columns: table => new
                {
                    CallingPlanId = table.Column<string>(nullable: false),
                    Minutes = table.Column<float>(nullable: false),
                    Cost = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallingPlans", x => x.CallingPlanId);
                });

            migrationBuilder.CreateTable(
                name: "DataPlans",
                columns: table => new
                {
                    DataPlanId = table.Column<string>(nullable: false),
                    Data = table.Column<int>(nullable: false),
                    Cost = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPlans", x => x.DataPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Managements",
                columns: table => new
                {
                    ManagementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managements", x => x.ManagementId);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhones",
                columns: table => new
                {
                    IMEI = table.Column<string>(nullable: false),
                    Model = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhones", x => x.IMEI);
                });

            migrationBuilder.CreateTable(
                name: "PhoneLines",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
                    PUK = table.Column<int>(nullable: false),
                    Contract = table.Column<string>(nullable: true),
                    PIN = table.Column<int>(nullable: false),
                    CallsDetails = table.Column<bool>(nullable: false),
                    SMSDetails = table.Column<bool>(nullable: false),
                    GPRSDetails = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLines", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "SmsPlans",
                columns: table => new
                {
                    SMSPlanId = table.Column<string>(nullable: false),
                    Messages = table.Column<int>(nullable: false),
                    Cost = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsPlans", x => x.SMSPlanId);
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
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
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
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
                name: "CostCenters",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ManagementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Code);
                    table.ForeignKey(
                        name: "FK_CostCenters_Managements_ManagementId",
                        column: x => x.ManagementId,
                        principalTable: "Managements",
                        principalColumn: "ManagementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallingPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
                    CallingPlanId = table.Column<string>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallingPlanAssignments", x => new { x.PhoneNumber, x.Year, x.Month, x.CallingPlanId });
                    table.ForeignKey(
                        name: "FK_CallingPlanAssignments_CallingPlans_CallingPlanId",
                        column: x => x.CallingPlanId,
                        principalTable: "CallingPlans",
                        principalColumn: "CallingPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CallingPlanAssignments_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
                    DataPlanId = table.Column<string>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPlanAssignments", x => new { x.PhoneNumber, x.Year, x.Month, x.DataPlanId });
                    table.ForeignKey(
                        name: "FK_DataPlanAssignments_DataPlans_DataPlanId",
                        column: x => x.DataPlanId,
                        principalTable: "DataPlans",
                        principalColumn: "DataPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataPlanAssignments_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GPRSs",
                columns: table => new
                {
                    GPRSId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneLinePhoneNumber = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    APN = table.Column<string>(nullable: true),
                    Volume = table.Column<string>(nullable: true),
                    VolFact = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Charge = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    Roaming = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPRSs", x => x.GPRSId);
                    table.ForeignKey(
                        name: "FK_GPRSs_PhoneLines_PhoneLinePhoneNumber",
                        column: x => x.PhoneLinePhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MobilePhoneCalls",
                columns: table => new
                {
                    MobilePhoneCallId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Addressee = table.Column<string>(nullable: true),
                    Duration = table.Column<float>(nullable: false),
                    TA = table.Column<float>(nullable: false),
                    LD = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Charge = table.Column<float>(nullable: false),
                    TotalCost = table.Column<float>(nullable: false),
                    RoamingCall = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhoneCalls", x => x.MobilePhoneCallId);
                    table.ForeignKey(
                        name: "FK_MobilePhoneCalls_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneLineSummaries",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    ImportByDetails = table.Column<float>(nullable: false),
                    Rent = table.Column<float>(nullable: false),
                    AirTime = table.Column<float>(nullable: false),
                    LongDistance = table.Column<float>(nullable: false),
                    DiscountTA = table.Column<float>(nullable: false),
                    DiscountLD = table.Column<float>(nullable: false),
                    ExtraCharges = table.Column<float>(nullable: false),
                    TotalCalls = table.Column<int>(nullable: false),
                    DayOfUse = table.Column<int>(nullable: false),
                    SubTotal = table.Column<float>(nullable: false),
                    SmsExpenses = table.Column<float>(nullable: false),
                    GprsExpenses = table.Column<float>(nullable: false),
                    RoamingExpenses = table.Column<float>(nullable: false),
                    RoamingSmsExpenses = table.Column<float>(nullable: false),
                    RoamingGprsExpenses = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneLineSummaries", x => new { x.PhoneNumber, x.Month, x.Year });
                    table.ForeignKey(
                        name: "FK_PhoneLineSummaries_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMS",
                columns: table => new
                {
                    SMSId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    E_R = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    Amount = table.Column<float>(nullable: false),
                    LD = table.Column<float>(nullable: false),
                    Discount = table.Column<float>(nullable: false),
                    Charge = table.Column<float>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    Roaming = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS", x => x.SMSId);
                    table.ForeignKey(
                        name: "FK_SMS_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmsPlanAssignments",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    SMSPlanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsPlanAssignments", x => new { x.PhoneNumber, x.Year, x.Month });
                    table.ForeignKey(
                        name: "FK_SmsPlanAssignments_PhoneLines_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "PhoneLines",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmsPlanAssignments_SmsPlans_SMSPlanId",
                        column: x => x.SMSPlanId,
                        principalTable: "SmsPlans",
                        principalColumn: "SMSPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CostCenterCode = table.Column<string>(nullable: true),
                    PersonalCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_CostCenters_CostCenterCode",
                        column: x => x.CostCenterCode,
                        principalTable: "CostCenters",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LandLinePhoneCalls",
                columns: table => new
                {
                    Extension = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Cost = table.Column<float>(nullable: false),
                    Duration = table.Column<float>(nullable: false),
                    Addressee = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandLinePhoneCalls", x => new { x.Extension, x.DateTime, x.EmployeeId });
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
                    IMEI = table.Column<string>(nullable: false),
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
                name: "PhoneLineEmployees",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(nullable: false),
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
                name: "IX_CallingPlanAssignments_CallingPlanId",
                table: "CallingPlanAssignments",
                column: "CallingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_ManagementId",
                table: "CostCenters",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_DataPlanAssignments_DataPlanId",
                table: "DataPlanAssignments",
                column: "DataPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CostCenterCode",
                table: "Employees",
                column: "CostCenterCode");

            migrationBuilder.CreateIndex(
                name: "IX_GPRSs_PhoneLinePhoneNumber",
                table: "GPRSs",
                column: "PhoneLinePhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LandLinePhoneCalls_EmployeeId",
                table: "LandLinePhoneCalls",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneCalls_PhoneNumber",
                table: "MobilePhoneCalls",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MobilePhoneEmployees_EmployeeId",
                table: "MobilePhoneEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneLineEmployees_EmployeeId",
                table: "PhoneLineEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_PhoneNumber",
                table: "SMS",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SmsPlanAssignments_SMSPlanId",
                table: "SmsPlanAssignments",
                column: "SMSPlanId");
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
                name: "Bills");

            migrationBuilder.DropTable(
                name: "CallingPlanAssignments");

            migrationBuilder.DropTable(
                name: "DataPlanAssignments");

            migrationBuilder.DropTable(
                name: "GPRSs");

            migrationBuilder.DropTable(
                name: "LandLinePhoneCalls");

            migrationBuilder.DropTable(
                name: "MobilePhoneCalls");

            migrationBuilder.DropTable(
                name: "MobilePhoneEmployees");

            migrationBuilder.DropTable(
                name: "PhoneLineEmployees");

            migrationBuilder.DropTable(
                name: "PhoneLineSummaries");

            migrationBuilder.DropTable(
                name: "SMS");

            migrationBuilder.DropTable(
                name: "SmsPlanAssignments");

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

            migrationBuilder.DropTable(
                name: "SmsPlans");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "Managements");
        }
    }
}
