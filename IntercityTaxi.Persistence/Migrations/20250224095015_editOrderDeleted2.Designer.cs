﻿// <auto-generated />
using System;
using IntercityTaxi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntercityTaxi.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250224095015_editOrderDeleted2")]
    partial class editOrderDeleted2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Order.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Cities", (string)null);
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedByRoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FromAddress")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("FromCityId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ToAddress")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ToCityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TripTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CreatedByRoleId");

                    b.HasIndex("FromCityId");

                    b.HasIndex("ToCityId");

                    b.HasIndex("TripTypeId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Order.TripType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TripTypes", (string)null);
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Client", b =>
                {
                    b.HasBaseType("IntercityTaxi.Domain.Models.User");

                    b.Property<float>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(3f);

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Order.Order", b =>
                {
                    b.HasOne("IntercityTaxi.Domain.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntercityTaxi.Domain.Models.UserRole", "CreatedByRole")
                        .WithMany()
                        .HasForeignKey("CreatedByRoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntercityTaxi.Domain.Models.Order.City", "FromCity")
                        .WithMany()
                        .HasForeignKey("FromCityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntercityTaxi.Domain.Models.Order.City", "ToCity")
                        .WithMany()
                        .HasForeignKey("ToCityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IntercityTaxi.Domain.Models.Order.TripType", "TripType")
                        .WithMany()
                        .HasForeignKey("TripTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("CreatedByRole");

                    b.Navigation("FromCity");

                    b.Navigation("ToCity");

                    b.Navigation("TripType");
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.User", b =>
                {
                    b.HasOne("IntercityTaxi.Domain.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("IntercityTaxi.Domain.Models.Client", b =>
                {
                    b.HasOne("IntercityTaxi.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("IntercityTaxi.Domain.Models.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
