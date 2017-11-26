using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace timpossible.Migrations
{
    public partial class photoadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhoto",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPhoto",
                table: "Tasks");
        }
    }
}
