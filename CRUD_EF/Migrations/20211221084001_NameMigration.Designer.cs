﻿// <auto-generated />
using System;
using EFCore.DbConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CRUD_EF.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20211221084001_NameMigration")]
    partial class NameMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EFCore.Model.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.HasKey("CourseId");

                    b.ToTable("course");
                });

            modelBuilder.Entity("EFCore.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f64384a6-d858-4798-a6c9-a1b2f34ba7d6"),
                            Age = 19,
                            Email = "user@gmail.com",
                            FullName = "user",
                            Role = 0
                        },
                        new
                        {
                            Id = new Guid("0b6d8b7b-c683-45b6-b600-7f733a77b4d7"),
                            Age = 19,
                            Email = "admin@gmail.com",
                            FullName = "admin",
                            Role = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}