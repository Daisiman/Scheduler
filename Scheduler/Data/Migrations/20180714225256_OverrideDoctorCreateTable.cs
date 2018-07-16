using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class OverrideDoctorCreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkHoursId",
                table: "Doctors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_WorkHoursId",
                table: "Doctors",
                column: "WorkHoursId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_WorkHours_WorkHoursId",
                table: "Doctors",
                column: "WorkHoursId",
                principalTable: "WorkHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_WorkHours_WorkHoursId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_WorkHoursId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "WorkHoursId",
                table: "Doctors");
        }
    }
}
