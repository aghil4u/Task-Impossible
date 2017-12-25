using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TaskImpossible.Migrations
{
    public partial class updatewithdatepretext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetDate",
                table: "Tasks",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ClosingDate",
                table: "Tasks",
                newName: "EndDate");

            migrationBuilder.AddColumn<string>(
                name: "DatePretext",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePretext",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Tasks",
                newName: "TargetDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Tasks",
                newName: "ClosingDate");
        }
    }
}
