﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Smeuj.Platform.Infrastructure.Database;

#nullable disable

namespace Smeuj.Platform.Infrastructure.Database.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20231111145123_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-rc.2.23480.1");

            modelBuilder.Entity("Smeuj.Platform.Domain.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("AuthorSince")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("DiscordId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("DiscordId")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Example", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ProcessedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("SmeuId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("SubmittedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SmeuId");

                    b.ToTable("Examples");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Inspiration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ProcessedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("SmeuId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("SubmittedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("SmeuId");

                    b.ToTable("Inspirations");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Smeu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("DiscordId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ProcessedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("SubmittedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("DiscordId")
                        .IsUnique();

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("Smeuj");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Example", b =>
                {
                    b.HasOne("Smeuj.Platform.Domain.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Smeuj.Platform.Domain.Smeu", null)
                        .WithMany("Examples")
                        .HasForeignKey("SmeuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Inspiration", b =>
                {
                    b.HasOne("Smeuj.Platform.Domain.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Smeuj.Platform.Domain.Smeu", null)
                        .WithMany("Inspirations")
                        .HasForeignKey("SmeuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Smeu", b =>
                {
                    b.HasOne("Smeuj.Platform.Domain.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Smeuj.Platform.Domain.Smeu", b =>
                {
                    b.Navigation("Examples");

                    b.Navigation("Inspirations");
                });
#pragma warning restore 612, 618
        }
    }
}