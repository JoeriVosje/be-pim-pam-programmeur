using Microsoft.EntityFrameworkCore.Migrations;

namespace PimPamProgrammeur.Data.Migrations
{
    public partial class duplicateSessionModuleAllowed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_ModuleId",
                table: "Sessions");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ModuleId",
                table: "Sessions",
                column: "ModuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_ModuleId",
                table: "Sessions");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ModuleId",
                table: "Sessions",
                column: "ModuleId",
                unique: true);
        }
    }
}
