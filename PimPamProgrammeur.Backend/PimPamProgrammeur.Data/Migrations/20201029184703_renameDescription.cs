using Microsoft.EntityFrameworkCore.Migrations;

namespace PimPamProgrammeur.Data.Migrations
{
    public partial class renameDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Answers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
