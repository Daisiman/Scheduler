using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class OverrideDoctorCreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "Doctors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Doctors");
        }
    }
}
