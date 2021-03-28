﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TokanPages.Backend.Database.Migrations
{
    public partial class AddAlbumTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoGears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BodyVendor = table.Column<string>(maxLength: 100, nullable: true),
                    BodyModel = table.Column<string>(maxLength: 100, nullable: true),
                    LensVendor = table.Column<string>(maxLength: 100, nullable: true),
                    LensName = table.Column<string>(maxLength: 60, nullable: true),
                    FocalLength = table.Column<int>(nullable: false),
                    ShutterSpeed = table.Column<string>(maxLength: 10, nullable: true),
                    Aperture = table.Column<decimal>(nullable: false),
                    FilmIso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoGears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PhotoGearId = table.Column<Guid>(nullable: false),
                    PhotoCategoryId = table.Column<Guid>(nullable: false),
                    Keywords = table.Column<string>(maxLength: 500, nullable: true),
                    PhotoUrl = table.Column<string>(maxLength: 255, nullable: false),
                    DateTaken = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_PhotoCategories",
                        column: x => x.PhotoCategoryId,
                        principalTable: "PhotoCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_PhotoGears",
                        column: x => x.PhotoGearId,
                        principalTable: "PhotoGears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PhotoId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Photos",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Albums_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_PhotoId",
                table: "Albums",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserId",
                table: "Albums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoCategoryId",
                table: "Photos",
                column: "PhotoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoGearId",
                table: "Photos",
                column: "PhotoGearId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PhotoCategories");

            migrationBuilder.DropTable(
                name: "PhotoGears");
        }
    }
}
