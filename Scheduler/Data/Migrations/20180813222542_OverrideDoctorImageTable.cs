using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Data.Migrations
{
    public partial class OverrideDoctorImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorImage_Doctors_Id",
                table: "DoctorImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorImage",
                table: "DoctorImage");

            migrationBuilder.RenameTable(
                name: "DoctorImage",
                newName: "DoctorImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorImages",
                table: "DoctorImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorImages_Doctors_Id",
                table: "DoctorImages",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorImages_Doctors_Id",
                table: "DoctorImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorImages",
                table: "DoctorImages");

            migrationBuilder.RenameTable(
                name: "DoctorImages",
                newName: "DoctorImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorImage",
                table: "DoctorImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorImage_Doctors_Id",
                table: "DoctorImage",
                column: "Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
