using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskImpossible.Migrations
{
    public partial class updatewithdatepretext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "TargetDate",
                "Tasks",
                "StartDate");

            migrationBuilder.RenameColumn(
                "ClosingDate",
                "Tasks",
                "EndDate");

            migrationBuilder.AddColumn<string>(
                "DatePretext",
                "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DatePretext",
                "Tasks");

            migrationBuilder.RenameColumn(
                "StartDate",
                "Tasks",
                "TargetDate");

            migrationBuilder.RenameColumn(
                "EndDate",
                "Tasks",
                "ClosingDate");
        }
    }
}