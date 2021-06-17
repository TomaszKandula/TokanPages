﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TokanPages.Backend.Database.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    ReadCount = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    IsActivated = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserAlias = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    IsActivated = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastLogged = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
