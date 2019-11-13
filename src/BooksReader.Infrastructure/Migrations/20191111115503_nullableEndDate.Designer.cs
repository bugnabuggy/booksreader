﻿// <auto-generated />
using System;
using BooksReader.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BooksReader.Infrastructure.Migrations
{
    [DbContext(typeof(BrDbContext))]
    [Migration("20191111115503_nullableEndDate")]
    partial class nullableEndDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BooksReader.Core.Entities.AuthorApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Approved");

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthorApplications");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.AuthorProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("AuthorName")
                        .HasMaxLength(256);

                    b.Property<string>("Description")
                        .HasMaxLength(3000);

                    b.Property<string>("SemanticUrl")
                        .HasMaxLength(256);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthorProfiles");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .HasMaxLength(3000);

                    b.Property<bool>("IsForSale");

                    b.Property<bool>("IsPublished");

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Picture");

                    b.Property<DateTime?>("Published");

                    b.Property<string>("SemanticUrl")
                        .HasMaxLength(256);

                    b.Property<int>("SubscriptionDurationDays");

                    b.Property<string>("Title")
                        .HasMaxLength(1000);

                    b.Property<bool>("Verified");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookChapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .HasMaxLength(3000);

                    b.Property<bool>("IsPublished");

                    b.Property<long>("Number");

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Title")
                        .HasMaxLength(1000);

                    b.Property<bool>("Verified");

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookChapters");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookChapterHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ChapterId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Title")
                        .HasMaxLength(1000);

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("ChaptersHistory");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description")
                        .HasMaxLength(3000);

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Picture");

                    b.Property<string>("Title")
                        .HasMaxLength(1000);

                    b.Property<long>("Version");

                    b.HasKey("Id");

                    b.ToTable("BooksHistory");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookId");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CurrencyId");

                    b.Property<Guid>("OwnerId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("BookPrices");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookId");

                    b.Property<string>("Comment");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookSubscriptions");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BrUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Language")
                        .HasMaxLength(20);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .HasMaxLength(250);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.LoginHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Browser");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Geolocation");

                    b.Property<string>("IpAddress");

                    b.Property<string>("Screen");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginHistory");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.PublicPage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid>("DomainId");

                    b.Property<int>("PageType");

                    b.Property<Guid?>("SeoInfoId");

                    b.Property<Guid?>("SubjectId");

                    b.Property<string>("UrlPath")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.HasIndex("SeoInfoId");

                    b.ToTable("PublicPages");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.SeoInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MetaAuthor")
                        .HasMaxLength(256);

                    b.Property<string>("MetaCopyright")
                        .HasMaxLength(256);

                    b.Property<string>("MetaDescription")
                        .HasMaxLength(256);

                    b.Property<string>("MetaKeywords")
                        .HasMaxLength(256);

                    b.Property<string>("MetaTitle")
                        .HasMaxLength(256);

                    b.Property<string>("OgDescription")
                        .HasMaxLength(256);

                    b.Property<string>("OgImage")
                        .HasMaxLength(256);

                    b.Property<string>("OgTitle")
                        .HasMaxLength(256);

                    b.Property<string>("OgType")
                        .HasMaxLength(256);

                    b.Property<string>("OgUrl")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("SeoInfos");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.TypeValue", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("LocalizationKey")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120);

                    b.Property<short>("TypeId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("TypeValues");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.TypesList", b =>
                {
                    b.Property<short>("Id");

                    b.Property<string>("LocalizationKey")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.ToTable("TypesLists");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.UserDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Certificate");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Protocol");

                    b.Property<Guid>("VerificationCode");

                    b.Property<DateTime?>("VerificationDate");

                    b.Property<DateTime?>("VerificationRequested");

                    b.Property<int>("VerificationType");

                    b.Property<bool>("Verified");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("UserDomains");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.AuthorApplication", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.AuthorProfile", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookChapter", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookChapterHistory", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BookChapter", "BookChapter")
                        .WithMany()
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookPrice", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.Book", "Book")
                        .WithMany("Prices")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BooksReader.Core.Entities.TypeValue", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.BookSubscription", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.Book")
                        .WithMany("Subscriptions")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.LoginHistory", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.PublicPage", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.UserDomain", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BooksReader.Core.Entities.SeoInfo", "SeoInfo")
                        .WithMany()
                        .HasForeignKey("SeoInfoId");
                });

            modelBuilder.Entity("BooksReader.Core.Entities.TypeValue", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.TypesList", "Type")
                        .WithMany("Values")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BooksReader.Core.Entities.UserDomain", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser", "User")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BooksReader.Core.Entities.BrUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BooksReader.Core.Entities.BrUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
