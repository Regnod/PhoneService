using Microsoft.EntityFrameworkCore.Migrations;

namespace Repo.Migrations
{
    public partial class a231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LandlinePhoneCallCost",
                table: "LandLinePhoneCalls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandlinePhoneCallCost",
                table: "LandLinePhoneCalls");
        }
    }
}
