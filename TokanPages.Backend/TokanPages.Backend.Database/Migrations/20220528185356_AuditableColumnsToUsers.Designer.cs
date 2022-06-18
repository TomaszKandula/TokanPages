﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220528185356_AuditableColumnsToUsers")]
    partial class AuditableColumnsToUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Albums", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.HasIndex("UserId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleCounts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("ReadCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleCounts");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleLikes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleLikes");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Articles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("bit");

                    b.Property<int>("ReadCount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.DefaultPermissions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("DefaultPermissions");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.HttpRequests", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestedHandlerName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SourceAddress")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("HttpRequests");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Permissions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoCategories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("PhotoCategories");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoGears", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Aperture")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BodyModel")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BodyVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("FilmIso")
                        .HasColumnType("int");

                    b.Property<int>("FocalLength")
                        .HasColumnType("int");

                    b.Property<string>("LensName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("LensVendor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShutterSpeed")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("PhotoGears");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Photos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTaken")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keywords")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("PhotoCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhotoGearId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhotoCategoryId");

                    b.HasIndex("PhotoGearId");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Name")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Subscribers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserAboutText")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserImageName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserVideoName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserPermissions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRefreshTokens", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ReplacedByToken")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserRefreshTokens");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserTokens", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActivationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ActivationIdEnds")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CryptedPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogged")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ResetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ResetIdEnds")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAlias")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Albums", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Photos", "Photo")
                        .WithMany("Albums")
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK_Albums_Photos")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("Albums")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Albums_Users");

                    b.Navigation("Photo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleCounts", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Articles", "Article")
                        .WithMany("ArticleCounts")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK_ArticleCounts_Articles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("ArticleCounts")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ArticleCounts_Users");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleLikes", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Articles", "Article")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK_ArticleLikes_Articles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("ArticleLikes")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ArticleLikes_Users");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Articles", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Articles_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.DefaultPermissions", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Permissions", "Permission")
                        .WithMany("DefaultPermissions")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_DefaultPermissions_Permissions")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Roles", "Role")
                        .WithMany("DefaultPermissions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_DefaultPermissions_Roles")
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Photos", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.PhotoCategories", "PhotoCategory")
                        .WithMany("Photos")
                        .HasForeignKey("PhotoCategoryId")
                        .HasConstraintName("FK_Photos_PhotoCategories")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.PhotoGears", "PhotoGear")
                        .WithMany("Photos")
                        .HasForeignKey("PhotoGearId")
                        .HasConstraintName("FK_Photos_PhotoGears")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Photos_Users")
                        .IsRequired();

                    b.Navigation("PhotoCategory");

                    b.Navigation("PhotoGear");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserInfo", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserInfoNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserInfo_Users")
                        .IsRequired();

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserPermissions", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Permissions", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_UserPermissions_Permissions")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserPermissions_Users")
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRefreshTokens", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("UserRefreshTokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRefreshTokens_Users")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRoles", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_Roles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_Users")
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserTokens", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserTokens_Users")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Articles", b =>
                {
                    b.Navigation("ArticleCounts");

                    b.Navigation("ArticleLikes");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Permissions", b =>
                {
                    b.Navigation("DefaultPermissions");

                    b.Navigation("UserPermissions");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoCategories", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoGears", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Photos", b =>
                {
                    b.Navigation("Albums");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Roles", b =>
                {
                    b.Navigation("DefaultPermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Users", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("ArticleCounts");

                    b.Navigation("ArticleLikes");

                    b.Navigation("Articles");

                    b.Navigation("Photos");

                    b.Navigation("UserInfoNavigation");

                    b.Navigation("UserPermissions");

                    b.Navigation("UserRefreshTokens");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
