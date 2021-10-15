﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModularRestaurant.Ratings.Infrastructure.EF;

namespace ModularRestaurant.Ratings.Infrastructure.EF.Migrations
{
    [DbContext(typeof(RatingsDbContext))]
    [Migration("20211015105957_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ratings")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModularRestaurant.Ratings.Domain.Entities.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("ModularRestaurant.Ratings.Domain.Entities.Restaurant", b =>
                {
                    b.OwnsMany("ModularRestaurant.Ratings.Domain.Entities.UserRating", "UserRatings", b1 =>
                        {
                            b1.Property<Guid>("RestaurantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.HasKey("RestaurantId", "Id");

                            b1.ToTable("UserRating");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");

                            b1.OwnsOne("ModularRestaurant.Ratings.Domain.Entities.Comment", "Comment", b2 =>
                                {
                                    b2.Property<Guid>("UserRatingRestaurantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("UserRatingId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Text")
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("Comment");

                                    b2.HasKey("UserRatingRestaurantId", "UserRatingId");

                                    b2.ToTable("UserRating");

                                    b2.WithOwner()
                                        .HasForeignKey("UserRatingRestaurantId", "UserRatingId");
                                });

                            b1.OwnsOne("ModularRestaurant.Ratings.Domain.Entities.Comment", "RestaurantReply", b2 =>
                                {
                                    b2.Property<Guid>("UserRatingRestaurantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("UserRatingId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Text")
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("RestaurantReply");

                                    b2.HasKey("UserRatingRestaurantId", "UserRatingId");

                                    b2.ToTable("UserRating");

                                    b2.WithOwner()
                                        .HasForeignKey("UserRatingRestaurantId", "UserRatingId");
                                });

                            b1.OwnsOne("ModularRestaurant.Ratings.Domain.Entities.Rating", "Rating", b2 =>
                                {
                                    b2.Property<Guid>("UserRatingRestaurantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("UserRatingId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Rating");

                                    b2.HasKey("UserRatingRestaurantId", "UserRatingId");

                                    b2.ToTable("UserRating");

                                    b2.WithOwner()
                                        .HasForeignKey("UserRatingRestaurantId", "UserRatingId");
                                });

                            b1.OwnsOne("ModularRestaurant.Shared.Domain.Types.UserId", "UserId", b2 =>
                                {
                                    b2.Property<Guid>("UserRatingRestaurantId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("UserRatingId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("UserId");

                                    b2.HasKey("UserRatingRestaurantId", "UserRatingId");

                                    b2.ToTable("UserRating");

                                    b2.WithOwner()
                                        .HasForeignKey("UserRatingRestaurantId", "UserRatingId");
                                });

                            b1.Navigation("Comment");

                            b1.Navigation("Rating");

                            b1.Navigation("RestaurantReply");

                            b1.Navigation("UserId");
                        });

                    b.Navigation("UserRatings");
                });
#pragma warning restore 612, 618
        }
    }
}
