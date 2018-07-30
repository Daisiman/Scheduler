using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class CreateDoctorWorkHoursTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorWorkHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    MondayFrom = table.Column<DateTime>(nullable: true),
                    MondayTo = table.Column<DateTime>(nullable: true),
                    TuesdayFrom = table.Column<DateTime>(nullable: true),
                    TuesdayTo = table.Column<DateTime>(nullable: true),
                    WednesdayFrom = table.Column<DateTime>(nullable: true),
                    WednesdayTo = table.Column<DateTime>(nullable: true),
                    ThursdayFrom = table.Column<DateTime>(nullable: true),
                    ThursdayTo = table.Column<DateTime>(nullable: true),
                    FridayFrom = table.Column<DateTime>(nullable: true),
                    FridayTo = table.Column<DateTime>(nullable: true),
                    SaturdayFrom = table.Column<DateTime>(nullable: true),
                    SaturdayTo = table.Column<DateTime>(nullable: true),
                    SundayFrom = table.Column<DateTime>(nullable: true),
                    SundayTo = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorWorkHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorWorkHours_Doctors_Id",
                        column: x => x.Id,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorWorkHours");
        }
    }
}
