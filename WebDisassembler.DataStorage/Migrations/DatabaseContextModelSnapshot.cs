﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebDisassembler.DataStorage.Utility;

#nullable disable

namespace WebDisassembler.DataStorage.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.FileReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CreatedAt")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsTemporary")
                        .HasColumnType("boolean");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserId");

                    b.ToTable("FileReferences");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.AuthenticationToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ExpiresBy")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Token", "UserId");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("AuthenticationToken");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id", "TenantId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6ad2c1f1-c06b-43c8-986b-cbaab6970c56"),
                            TenantId = new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"),
                            IsAdministrator = true,
                            Name = "Admin Role",
                            Permissions = "[]"
                        });
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Public")
                        .HasColumnType("boolean");

                    b.Property<string>("Subdomain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"),
                            Name = "Admin Tenant",
                            Public = false,
                            Subdomain = "admin",
                            UserId = new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4")
                        });
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.TenantUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantUser");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4"),
                            TenantId = new Guid("5a8a414f-e050-459f-aa4d-3bc41037ce4f"),
                            RoleId = new Guid("6ad2c1f1-c06b-43c8-986b-cbaab6970c56")
                        });
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a1bbbc3f-8358-4a7f-b214-9b155b6e97c4"),
                            Email = "admin@webdisassembler.io",
                            PasswordHash = "123",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.FileReference", b =>
                {
                    b.HasOne("WebDisassembler.DataStorage.Models.Identity.Tenant", "Tenant")
                        .WithMany("FileReferences")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebDisassembler.DataStorage.Models.Identity.User", "User")
                        .WithMany("FileReferences")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.AuthenticationToken", b =>
                {
                    b.HasOne("WebDisassembler.DataStorage.Models.Identity.User", "User")
                        .WithMany("AuthenticationTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.TenantUser", b =>
                {
                    b.HasOne("WebDisassembler.DataStorage.Models.Identity.Tenant", "Tenant")
                        .WithMany("TenantUsers")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebDisassembler.DataStorage.Models.Identity.User", "User")
                        .WithMany("TenantUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.Tenant", b =>
                {
                    b.Navigation("FileReferences");

                    b.Navigation("TenantUsers");
                });

            modelBuilder.Entity("WebDisassembler.DataStorage.Models.Identity.User", b =>
                {
                    b.Navigation("AuthenticationTokens");

                    b.Navigation("FileReferences");

                    b.Navigation("TenantUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
