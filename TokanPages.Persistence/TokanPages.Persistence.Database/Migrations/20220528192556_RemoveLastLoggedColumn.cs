﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TokanPages.Persistence.Database.Migrations
{
    public partial class RemoveLastLoggedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLogged",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogged",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
