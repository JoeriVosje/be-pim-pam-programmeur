using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PimPamProgrammeur.Data.Migrations
{
    public partial class addedComponentToResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                table: "Results",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Results_ComponentId",
                table: "Results",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Components_ComponentId",
                table: "Results",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Components_ComponentId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_ComponentId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Results");
        }
    }
}
