using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_EF.Migrations
{
    public partial class FK_Course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_course",
                table: "course");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "course");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "course",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_course",
                table: "course",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CourseId",
                table: "users",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_course_CourseId",
                table: "users",
                column: "CourseId",
                principalTable: "course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_course_CourseId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_CourseId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course",
                table: "course");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "course");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "course",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_course",
                table: "course",
                column: "Id");
        }
    }
}
