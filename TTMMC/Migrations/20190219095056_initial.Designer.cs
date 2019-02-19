﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TTMMC_ESSETRE.Services;

namespace TTMMC_ESSETRE.Migrations
{
    [DbContext(typeof(TTMMCContext))]
    [Migration("20190219095056_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.Layout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<int?>("ItemCode");

                    b.Property<string>("ItemDescription");

                    b.Property<long>("LayoutNumber");

                    b.Property<int?>("LayoutPhase");

                    b.Property<int?>("LayoutSetRecordId");

                    b.Property<string>("LayoutType");

                    b.Property<int>("Machine");

                    b.Property<string>("MachineName");

                    b.Property<int?>("MachineNumber");

                    b.Property<int?>("Meters");

                    b.Property<int?>("Quantity");

                    b.Property<DateTime>("StartTimestamp");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("LayoutSetRecordId");

                    b.ToTable("Layouts");
                });

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.LayoutRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("LayoutId");

                    b.HasKey("Id");

                    b.HasIndex("LayoutId");

                    b.ToTable("LayoutsActRecords");
                });

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.LayoutRecordField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.Property<int?>("LayoutRecordId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("LayoutRecordId");

                    b.ToTable("LayoutsActRecordsFields");
                });

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.Layout", b =>
                {
                    b.HasOne("TTMMC_ESSETRE.Models.DBModels.LayoutRecord", "LayoutSetRecord")
                        .WithMany()
                        .HasForeignKey("LayoutSetRecordId");
                });

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.LayoutRecord", b =>
                {
                    b.HasOne("TTMMC_ESSETRE.Models.DBModels.Layout")
                        .WithMany("LayoutActRecords")
                        .HasForeignKey("LayoutId");
                });

            modelBuilder.Entity("TTMMC_ESSETRE.Models.DBModels.LayoutRecordField", b =>
                {
                    b.HasOne("TTMMC_ESSETRE.Models.DBModels.LayoutRecord")
                        .WithMany("Fields")
                        .HasForeignKey("LayoutRecordId");
                });
#pragma warning restore 612, 618
        }
    }
}
