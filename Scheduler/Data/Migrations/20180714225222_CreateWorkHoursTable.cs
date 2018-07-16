using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Data.Migrations
{
    public partial class CreateWorkHoursTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "Doctors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Monday = table.Column<DateTime>(nullable: false),
                    Tuesday = table.Column<DateTime>(nullable: false),
                    Wednesday = table.Column<DateTime>(nullable: false),
                    Thursday = table.Column<DateTime>(nullable: false),
                    Friday = table.Column<DateTime>(nullable: false),
                    Saturday = table.Column<DateTime>(nullable: false),
                    Sunday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHours", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkHours");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "Doctors");
        }
    }
}
