using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Scheduler.Data.Migrations
{
    public partial class OverrideBlackListModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackList",
                table: "BlackList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlackList");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BlackList",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BlackList_DateAdded_UserId",
                table: "BlackList",
                columns: new[] { "DateAdded", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackList",
                table: "BlackList",
                columns: new[] { "UserId", "DateAdded" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlackList_AspNetUsers_UserId",
                table: "BlackList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackList_AspNetUsers_UserId",
                table: "BlackList");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BlackList_DateAdded_UserId",
                table: "BlackList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlackList",
                table: "BlackList");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BlackList",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BlackList",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlackList",
                table: "BlackList",
                column: "Id");
        }
    }
}
