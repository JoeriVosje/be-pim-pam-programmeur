using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PimPamProgrammeur.Data.Migrations
{
    public partial class fixNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classrooms_ClassroomId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Succes",
                table: "Results");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassroomId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "Results",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "CorrectAnswerId",
                table: "Components",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classrooms_ClassroomId",
                table: "Users",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Classrooms_ClassroomId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "Results");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClassroomId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Succes",
                table: "Results",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "CorrectAnswerId",
                table: "Components",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classrooms_ClassroomId",
                table: "Users",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
