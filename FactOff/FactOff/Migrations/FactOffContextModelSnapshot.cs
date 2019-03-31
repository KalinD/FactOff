﻿// <auto-generated />
using System;
using FactOff.Models.DB2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FactOff.Migrations
{
    [DbContext(typeof(FactOffContext))]
    partial class FactOffContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FactOff.Models.DB2.Fact", b =>
                {
                    b.Property<Guid>("FactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Context");

                    b.Property<Guid?>("CreatorUserId")
                        .IsRequired();

                    b.Property<int>("RateCount");

                    b.Property<float>("Rating");

                    b.HasKey("FactId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Facts");
                });

            modelBuilder.Entity("FactOff.Models.DB2.FactsTags", b =>
                {
                    b.Property<Guid>("FactId");

                    b.Property<Guid>("TagId");

                    b.HasKey("FactId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("FactsTags");
                });

            modelBuilder.Entity("FactOff.Models.DB2.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FactOff.Models.DB2.Theme", b =>
                {
                    b.Property<Guid>("ThemeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ThemeId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("FactOff.Models.DB2.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FactOff.Models.DB2.UserFavoritesFacts", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("FactId");

                    b.HasKey("UserId", "FactId");

                    b.HasIndex("FactId");

                    b.ToTable("UserFavoritesFacts");
                });

            modelBuilder.Entity("FactOff.Models.DB2.UserFavoriteThemes", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("ThemeId");

                    b.HasKey("UserId", "ThemeId");

                    b.HasIndex("ThemeId");

                    b.ToTable("UserFavoriteThemes");
                });

            modelBuilder.Entity("FactOff.Models.DB2.Fact", b =>
                {
                    b.HasOne("FactOff.Models.DB2.User", "Creator")
                        .WithMany("CreatedFacts")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("FactOff.Models.DB2.FactsTags", b =>
                {
                    b.HasOne("FactOff.Models.DB2.Fact", "Fact")
                        .WithMany("Tags")
                        .HasForeignKey("FactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FactOff.Models.DB2.Tag", "Tag")
                        .WithMany("Facts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FactOff.Models.DB2.UserFavoritesFacts", b =>
                {
                    b.HasOne("FactOff.Models.DB2.Fact", "Fact")
                        .WithMany("Users")
                        .HasForeignKey("FactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FactOff.Models.DB2.User", "User")
                        .WithMany("FavoriteFacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FactOff.Models.DB2.UserFavoriteThemes", b =>
                {
                    b.HasOne("FactOff.Models.DB2.Theme", "Theme")
                        .WithMany("Users")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FactOff.Models.DB2.User", "User")
                        .WithMany("FavoriteThemes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}