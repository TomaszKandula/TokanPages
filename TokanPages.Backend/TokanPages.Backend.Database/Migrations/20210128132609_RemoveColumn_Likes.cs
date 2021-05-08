using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TokanPages.Backend.Database.Migrations
{
    public partial class RemoveColumn_Likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("731a6665-1c80-44e5-af6e-4d8331efe028"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("7494688a-994c-4905-9073-8c68811ec839"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("f6493f03-0e85-466c-970b-6f1a07001173"));

            migrationBuilder.DeleteData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: new Guid("098a9c38-c31d-4a29-b5a7-5d02a1a1f7ae"));

            migrationBuilder.DeleteData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: new Guid("8a40f1b0-f983-4e51-9bfe-aeb5a5aee1bf"));

            migrationBuilder.DeleteData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: new Guid("ec8dd29c-464c-4e7a-897c-ce0ace2619ec"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("08be222f-dfcd-42db-8509-fd78ef09b912"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d047a17-9865-47f1-acb3-53b08539e7c9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d6365db3-d464-4146-857b-d8476f46553c"));

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CreatedAt", "Description", "IsPublished", "Likes", "ReadCount", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("731a6665-1c80-44e5-af6e-4d8331efe028"), new DateTime(2020, 1, 10, 12, 15, 15, 0, DateTimeKind.Unspecified), "Description", false, 0, 0, "Title", null },
                    { new Guid("7494688a-994c-4905-9073-8c68811ec839"), new DateTime(2020, 1, 25, 5, 9, 19, 0, DateTimeKind.Unspecified), "Description", false, 0, 0, "Title", null },
                    { new Guid("f6493f03-0e85-466c-970b-6f1a07001173"), new DateTime(2020, 9, 12, 22, 1, 33, 0, DateTimeKind.Unspecified), "Description", false, 0, 0, "Title", null }
                });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "Count", "Email", "IsActivated", "LastUpdated", "Registered" },
                values: new object[,]
                {
                    { new Guid("098a9c38-c31d-4a29-b5a7-5d02a1a1f7ae"), 0, "ester1990@gmail.com", false, null, new DateTime(2020, 1, 10, 12, 15, 15, 0, DateTimeKind.Unspecified) },
                    { new Guid("ec8dd29c-464c-4e7a-897c-ce0ace2619ec"), 0, "tokan@dfds.com", false, null, new DateTime(2020, 1, 25, 5, 9, 19, 0, DateTimeKind.Unspecified) },
                    { new Guid("8a40f1b0-f983-4e51-9bfe-aeb5a5aee1bf"), 0, "admin@tomkandula.com", false, null, new DateTime(2020, 9, 12, 22, 1, 33, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "FirstName", "IsActivated", "LastLogged", "LastName", "LastUpdated", "Registered", "UserAlias" },
                values: new object[,]
                {
                    { new Guid("08be222f-dfcd-42db-8509-fd78ef09b912"), "ester.exposito@gmail.com", "Ester", true, new DateTime(2020, 1, 10, 15, 0, 33, 0, DateTimeKind.Unspecified), "Exposito", null, new DateTime(2020, 1, 10, 12, 15, 15, 0, DateTimeKind.Unspecified), "ester" },
                    { new Guid("d6365db3-d464-4146-857b-d8476f46553c"), "tokan@dfds.com", "Tom", true, new DateTime(2020, 3, 22, 12, 0, 15, 0, DateTimeKind.Unspecified), "Tom", new DateTime(2020, 5, 21, 5, 9, 11, 0, DateTimeKind.Unspecified), new DateTime(2020, 1, 25, 5, 9, 19, 0, DateTimeKind.Unspecified), "tokan" },
                    { new Guid("3d047a17-9865-47f1-acb3-53b08539e7c9"), "dummy@dummy.net", "Dummy", true, new DateTime(2020, 5, 12, 15, 5, 3, 0, DateTimeKind.Unspecified), "Dummy", null, new DateTime(2020, 9, 12, 22, 1, 33, 0, DateTimeKind.Unspecified), "dummy" }
                });
        }
    }
}
