using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_EF.Migrations
{
    public partial class NameMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Age", "Email", "FullName", "Role" },
                values: new object[,]
                {
                    { new Guid("f64384a6-d858-4798-a6c9-a1b2f34ba7d6"), 19, "user@gmail.com", "user", 0 },
                    { new Guid("0b6d8b7b-c683-45b6-b600-7f733a77b4d7"), 19, "admin@gmail.com", "admin", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
