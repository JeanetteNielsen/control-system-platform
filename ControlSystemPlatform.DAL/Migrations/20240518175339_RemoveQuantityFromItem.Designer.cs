﻿// <auto-generated />
using System;
using ControlSystemPlatform.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlSystemPlatform.DAL.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    [Migration("20240518175339_RemoveQuantityFromItem")]
    partial class RemoveQuantityFromItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.ErrorHandlingProtocol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Issue")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<string>("ResolutionSteps")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<Guid?>("TransportOrderEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransportOrderEntityId");

                    b.ToTable("ErrorHandlingProtocol");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.Events", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventDescription")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TransportOrderEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TransportOrderEntityId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<string>("SKU")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SpecialHandlingInstructions")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<decimal>("Weight")
                        .HasPrecision(8, 3)
                        .HasColumnType("decimal(8,3)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.OrderItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("TransportOrderEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("TransportOrderEntityId");

                    b.ToTable("OrderItemEntity");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.RouteInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlternatePath")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<string>("SpecialNavigationInstructions")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<Guid>("TransportOrderEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Waypoints")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TransportOrderEntityId")
                        .IsUnique();

                    b.ToTable("RouteInfo");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedResource")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("DestinationLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HandlingInstructions")
                        .HasMaxLength(265)
                        .HasColumnType("nvarchar(265)");

                    b.Property<string>("HandlingStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RequestedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RequesterOrderReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RequiredCompletionTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SourceLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("RequesterOrderReference")
                        .IsUnique();

                    b.ToTable("TransportOrder");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.ErrorHandlingProtocol", b =>
                {
                    b.HasOne("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", null)
                        .WithMany("ErrorHandlingProtocols")
                        .HasForeignKey("TransportOrderEntityId");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.Events", b =>
                {
                    b.HasOne("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", null)
                        .WithMany("Timestamps")
                        .HasForeignKey("TransportOrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.Item", b =>
                {
                    b.OwnsOne("ControlSystemPlatform.DAL.Enities.Dimensions", "Dimensions", b1 =>
                        {
                            b1.Property<Guid>("ItemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Height")
                                .HasPrecision(8, 2)
                                .HasColumnType("decimal(8,2)")
                                .HasColumnName("Height");

                            b1.Property<decimal>("Length")
                                .HasPrecision(8, 2)
                                .HasColumnType("decimal(8,2)")
                                .HasColumnName("Length");

                            b1.Property<decimal>("Width")
                                .HasPrecision(8, 2)
                                .HasColumnType("decimal(8,2)")
                                .HasColumnName("Width");

                            b1.HasKey("ItemId");

                            b1.ToTable("Items");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.Navigation("Dimensions");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.OrderItemEntity", b =>
                {
                    b.HasOne("ControlSystemPlatform.DAL.Enities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", null)
                        .WithMany("Items")
                        .HasForeignKey("TransportOrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.RouteInfo", b =>
                {
                    b.HasOne("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", null)
                        .WithOne("RoutingInformation")
                        .HasForeignKey("ControlSystemPlatform.DAL.Enities.RouteInfo", "TransportOrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ControlSystemPlatform.DAL.Enities.TransportOrderEntity", b =>
                {
                    b.Navigation("ErrorHandlingProtocols");

                    b.Navigation("Items");

                    b.Navigation("RoutingInformation");

                    b.Navigation("Timestamps");
                });
#pragma warning restore 612, 618
        }
    }
}