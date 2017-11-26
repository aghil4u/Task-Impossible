using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace timpossible.Migrations
{
    public partial class useradded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Base = table.Column<string>(nullable: true),
                    BaseCurrency = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    Capabilities = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    DisplayImage = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LastVisitedDate = table.Column<DateTime>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Lon = table.Column<double>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: false),
                    OverallRating = table.Column<int>(nullable: false),
                    Qualifications = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Tasker = table.Column<bool>(nullable: false),
                    TasksCompleted = table.Column<int>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
