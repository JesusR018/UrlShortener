﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using URL_Shortener.Data;

#nullable disable

namespace URL_Shortener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240320130525_AddUrlsTable")]
    partial class AddUrlsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("URL_Shortener.Models.UrlModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ClickCounter")
                        .HasColumnType("int");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OriginalUrl")
                        .IsUnique();

                    b.ToTable("Urls");
                });
#pragma warning restore 612, 618
        }
    }
}
