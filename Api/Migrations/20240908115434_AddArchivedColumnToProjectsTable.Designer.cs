﻿// <auto-generated />
using System;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(TodoManagerContext))]
    [Migration("20240908115434_AddArchivedColumnToProjectsTable")]
    partial class AddArchivedColumnToProjectsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Database.ExceptionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("endpoint");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("error_message");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("header");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("method");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("stack_trace");

                    b.HasKey("Id");

                    b.ToTable("exception_log", (string)null);
                });

            modelBuilder.Entity("Api.Models.Database.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Archived")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("archived");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("project", (string)null);
                });

            modelBuilder.Entity("Api.Models.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("login");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
