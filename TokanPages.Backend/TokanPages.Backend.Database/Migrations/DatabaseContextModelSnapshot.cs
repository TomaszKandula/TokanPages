﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserPhotos", b =>
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

                    b.ToTable("UserPhotos");
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

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
                    b.HasOne("TokanPages.Backend.Domain.Entities.UserPhotos", "UserPhotoNavigation")
                        .WithMany("AlbumsNavigation")
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK_Albums_UserPhotos")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("AlbumsNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Albums_Users");

                    b.Navigation("UserNavigation");

                    b.Navigation("UserPhotoNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleCounts", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Articles", "ArticleNavigation")
                        .WithMany("ArticleCountsNavigation")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK_ArticleCounts_Articles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("ArticleCountsNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ArticleCounts_Users");

                    b.Navigation("ArticleNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.ArticleLikes", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Articles", "ArticleNavigation")
                        .WithMany("ArticleLikesNavigation")
                        .HasForeignKey("ArticleId")
                        .HasConstraintName("FK_ArticleLikes_Articles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("ArticleLikesNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ArticleLikes_Users");

                    b.Navigation("ArticleNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Articles", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("ArticlesNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Articles_Users");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.DefaultPermissions", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Permissions", "PermissionNavigation")
                        .WithMany("DefaultPermissionsNavigation")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_DefaultPermissions_Permissions")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Roles", "RoleNavigation")
                        .WithMany("DefaultPermissionsNavigation")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_DefaultPermissions_Roles")
                        .IsRequired();

                    b.Navigation("PermissionNavigation");

                    b.Navigation("RoleNavigation");
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
                    b.HasOne("TokanPages.Backend.Domain.Entities.Permissions", "PermissionNavigation")
                        .WithMany("UserPermissionsNavigation")
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_UserPermissions_Permissions")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserPermissionsNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserPermissions_Users")
                        .IsRequired();

                    b.Navigation("PermissionNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserPhotos", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.PhotoCategories", "PhotoCategoryNavigation")
                        .WithMany("UserPhotosNavigation")
                        .HasForeignKey("PhotoCategoryId")
                        .HasConstraintName("FK_UserPhotos_PhotoCategories")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.PhotoGears", "PhotoGearNavigation")
                        .WithMany("UserPhotosNavigation")
                        .HasForeignKey("PhotoGearId")
                        .HasConstraintName("FK_UserPhotos_PhotoGears")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserPhotosNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserPhotos_Users")
                        .IsRequired();

                    b.Navigation("PhotoCategoryNavigation");

                    b.Navigation("PhotoGearNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRefreshTokens", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserRefreshTokensNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRefreshTokens_Users")
                        .IsRequired();

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserRoles", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Roles", "RoleNavigation")
                        .WithMany("UserRolesNavigation")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_Roles")
                        .IsRequired();

                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserRolesNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_Users")
                        .IsRequired();

                    b.Navigation("RoleNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserTokens", b =>
                {
                    b.HasOne("TokanPages.Backend.Domain.Entities.Users", "UserNavigation")
                        .WithMany("UserTokensNavigation")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserTokens_Users")
                        .IsRequired();

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Articles", b =>
                {
                    b.Navigation("ArticleCountsNavigation");

                    b.Navigation("ArticleLikesNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Permissions", b =>
                {
                    b.Navigation("DefaultPermissionsNavigation");

                    b.Navigation("UserPermissionsNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoCategories", b =>
                {
                    b.Navigation("UserPhotosNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.PhotoGears", b =>
                {
                    b.Navigation("UserPhotosNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Roles", b =>
                {
                    b.Navigation("DefaultPermissionsNavigation");

                    b.Navigation("UserRolesNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.UserPhotos", b =>
                {
                    b.Navigation("AlbumsNavigation");
                });

            modelBuilder.Entity("TokanPages.Backend.Domain.Entities.Users", b =>
                {
                    b.Navigation("AlbumsNavigation");

                    b.Navigation("ArticleCountsNavigation");

                    b.Navigation("ArticleLikesNavigation");

                    b.Navigation("ArticlesNavigation");

                    b.Navigation("UserInfoNavigation");

                    b.Navigation("UserPermissionsNavigation");

                    b.Navigation("UserPhotosNavigation");

                    b.Navigation("UserRefreshTokensNavigation");

                    b.Navigation("UserRolesNavigation");

                    b.Navigation("UserTokensNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
