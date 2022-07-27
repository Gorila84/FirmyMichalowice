﻿// <auto-generated />
using System;
using FirmyMichalowice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FirmyMichalowice.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220331211523_entry")]
    partial class entry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("FirmyMichalowice.Model.CookieConsent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("UserIP")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CookieConsents");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Municipalitie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsConsentToUse")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Municipalities");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.PKD", b =>
                {
                    b.Property<string>("Nazwa")
                        .HasColumnType("longtext");

                    b.Property<string>("Symbol")
                        .HasColumnType("longtext");

                    b.ToTable("PKD");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("longblob");

                    b.Property<bool>("IsMain")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discription")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AdditionalAddress")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyName")
                        .HasColumnType("longtext");

                    b.Property<string>("CompanyType")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("longtext");

                    b.Property<int>("EntryCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LongDescription")
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<DateTime>("Modify")
                        .HasColumnType("datetime");

                    b.Property<string>("Municipalitie")
                        .HasColumnType("longtext");

                    b.Property<string>("NIP")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("OfficeCity")
                        .HasColumnType("longtext");

                    b.Property<string>("OfficeMunicipalitie")
                        .HasColumnType("longtext");

                    b.Property<string>("OfficePostalCode")
                        .HasColumnType("longtext");

                    b.Property<string>("OfficeStreet")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<string>("Rodo")
                        .HasColumnType("longtext");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WebSite")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Offer", b =>
                {
                    b.HasOne("FirmyMichalowice.Model.User", "User")
                        .WithMany("Offers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FirmyMichalowice.Model.Photo", b =>
                {
                    b.HasOne("FirmyMichalowice.Model.User", null)
                        .WithOne("Photo")
                        .HasForeignKey("FirmyMichalowice.Model.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FirmyMichalowice.Model.User", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("Photo");
                });
#pragma warning restore 612, 618
        }
    }
}