using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_EF.Migrations
{
    public partial class RoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_course_CourseId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_CourseId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "users",
                type: "uuid",
                nullable: true);

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
    }
}
