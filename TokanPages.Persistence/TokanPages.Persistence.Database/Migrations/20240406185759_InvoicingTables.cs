﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TokanPages.Persistence.Database.Migrations
{
    public partial class InvoicingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchInvoicesProcessing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchProcessingTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchInvoicesProcessing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssuedInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InvoiceData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssuedInvoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SwiftNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(28)", maxLength: 28, nullable: false),
                    CurrencyCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBankAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VatNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CurrencyCode = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompanies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VatNumberPatterns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Pattern = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatNumberPatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    VoucherDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTerms = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerVatNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PostalArea = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProcessBatchKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceTemplateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserBankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchInvoices_BatchInvoicesProcessing",
                        column: x => x.ProcessBatchKey,
                        principalTable: "BatchInvoicesProcessing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchInvoices_UserBankAccount",
                        column: x => x.UserBankAccountId,
                        principalTable: "UserBankAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchInvoices_UserCompanies",
                        column: x => x.UserCompanyId,
                        principalTable: "UserCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BatchInvoices_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BatchInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatchInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ItemQuantity = table.Column<int>(type: "int", nullable: false),
                    ItemQuantityUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ItemAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemDiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchInvoiceItems_BatchInvoices",
                        column: x => x.BatchInvoiceId,
                        principalTable: "BatchInvoices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchInvoiceItems_BatchInvoiceId",
                table: "BatchInvoiceItems",
                column: "BatchInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInvoices_ProcessBatchKey",
                table: "BatchInvoices",
                column: "ProcessBatchKey");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInvoices_UserBankAccountId",
                table: "BatchInvoices",
                column: "UserBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInvoices_UserCompanyId",
                table: "BatchInvoices",
                column: "UserCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInvoices_UserId",
                table: "BatchInvoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuedInvoices_UserId",
                table: "IssuedInvoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBankAccounts_UserId",
                table: "UserBankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_UserId",
                table: "UserCompanies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchInvoiceItems");

            migrationBuilder.DropTable(
                name: "InvoiceTemplates");

            migrationBuilder.DropTable(
                name: "IssuedInvoices");

            migrationBuilder.DropTable(
                name: "VatNumberPatterns");

            migrationBuilder.DropTable(
                name: "BatchInvoices");

            migrationBuilder.DropTable(
                name: "BatchInvoicesProcessing");

            migrationBuilder.DropTable(
                name: "UserBankAccounts");

            migrationBuilder.DropTable(
                name: "UserCompanies");
        }
    }
}
