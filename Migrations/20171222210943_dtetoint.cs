using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskImpossible.Migrations
{
    public partial class dtetoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                "DatePretext",
                "Tasks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "DatePretext",
                "Tasks",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}