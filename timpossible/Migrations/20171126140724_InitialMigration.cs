using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace timpossible.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    ClosingDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Lon = table.Column<double>(nullable: false),
                    NegotiationMarker = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    PaymentTerms = table.Column<string>(nullable: true),
                    Radius = table.Column<double>(nullable: false),
                    Renumeration = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    TargetDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
