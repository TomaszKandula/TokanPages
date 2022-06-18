﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TokanPages.Backend.Database.Migrations
{
    public partial class RenamePhotoIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_UserPhotos",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_PhotoId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Albums");

            migrationBuilder.AddColumn<Guid>(
                name: "UserPhotoId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_UserPhotoId",
                table: "Albums",
                column: "UserPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_UserPhotos",
                table: "Albums",
                column: "UserPhotoId",
                principalTable: "UserPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_UserPhotos",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_UserPhotoId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "UserPhotoId",
                table: "Albums");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Albums_PhotoId",
                table: "Albums",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_UserPhotos",
                table: "Albums",
                column: "PhotoId",
                principalTable: "UserPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
