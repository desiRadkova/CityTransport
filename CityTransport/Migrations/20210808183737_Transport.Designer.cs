﻿// <auto-generated />
using System;
using CityTransport.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CityTransport.Migrations
{
    [DbContext(typeof(CityTransportDbContext))]
    [Migration("20210808183737_Transport")]
    partial class Transport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CityTransport.Data.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardNumber");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("StandartPrice");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Children", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildrenCardNumber");

                    b.Property<string>("ChildrenFirstName");

                    b.Property<string>("ChildrenId");

                    b.Property<string>("ChildrenLastName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Childrens");
                });

            modelBuilder.Entity("CityTransport.Data.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityID");

                    b.Property<string>("CityName");

                    b.Property<int>("CityPostCode");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("CityTransport.Data.Models.MyInvoices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("StandartPrice");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TransportId");

                    b.Property<string>("TransportKind");

                    b.Property<string>("TransportNumber");

                    b.Property<string>("TransportType");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("TransportId");

                    b.HasIndex("UserId");

                    b.ToTable("MyInvoices");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId");

                    b.Property<int>("Duration");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("StandartPrice");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TransportId");

                    b.Property<string>("TransportKind");

                    b.Property<string>("TransportNumber");

                    b.Property<string>("TransportType");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("TransportId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildrenCardNumber");

                    b.Property<string>("ChildrenFirstName");

                    b.Property<string>("ChildrenId");

                    b.Property<string>("ChildrenLastName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("CityTransport.Data.Models.SpecialOffers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OfferDescription");

                    b.Property<string>("OfferName");

                    b.Property<double>("OfferPrice");

                    b.Property<string>("RoleID");

                    b.Property<int>("SpecialOffersID");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SpecialOffers");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName");

                    b.Property<int>("TransportID");

                    b.Property<string>("TransportKind");

                    b.Property<string>("TransportNumber");

                    b.Property<string>("TransportType");

                    b.HasKey("Id");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

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

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CityTransport.Data.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City");

                    b.Property<int?>("CityNameId");

                    b.Property<long>("EGN");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Role");

                    b.Property<int>("SpecialOfferId");

                    b.HasIndex("CityNameId");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Card", b =>
                {
                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithOne("Card")
                        .HasForeignKey("CityTransport.Data.Models.Card", "UserId");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Children", b =>
                {
                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CityTransport.Data.Models.MyInvoices", b =>
                {
                    b.HasOne("CityTransport.Data.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("CityTransport.Data.Models.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("TransportId");

                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Order", b =>
                {
                    b.HasOne("CityTransport.Data.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");

                    b.HasOne("CityTransport.Data.Models.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("TransportId");

                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CityTransport.Data.Models.Parent", b =>
                {
                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CityTransport.Data.Models.SpecialOffers", b =>
                {
                    b.HasOne("CityTransport.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CityTransport.Data.Models.User", b =>
                {
                    b.HasOne("CityTransport.Data.Models.City", "CityName")
                        .WithMany()
                        .HasForeignKey("CityNameId");
                });
#pragma warning restore 612, 618
        }
    }
}
