﻿// <auto-generated />
using System;
using AdminPolizasAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdminPolizasAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240202221315_UpdateDataCobertura")]
    partial class UpdateDataCobertura
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AdminPolizasAPI.Entidades.Cobertura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Cerraduras")
                        .HasColumnType("bit");

                    b.Property<bool?>("CristalesLaterales")
                        .HasColumnType("bit");

                    b.Property<bool?>("DestruccionTotalAccidentes")
                        .HasColumnType("bit");

                    b.Property<bool?>("LunetasParabrisas")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool?>("ResponsabilidadCivil")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Coberturas");
                });

            modelBuilder.Entity("AdminPolizasAPI.Entidades.Poliza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Polizas");
                });

            modelBuilder.Entity("AdminPolizasAPI.Entidades.PolizasCoberturas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CoberturaId")
                        .HasColumnType("int");

                    b.Property<decimal>("MontoAsegurado")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("PolizaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CoberturaId");

                    b.HasIndex("PolizaId");

                    b.ToTable("PolizasCoberturas");
                });

            modelBuilder.Entity("AdminPolizasAPI.Entidades.PolizasCoberturas", b =>
                {
                    b.HasOne("AdminPolizasAPI.Entidades.Cobertura", "Cobertura")
                        .WithMany()
                        .HasForeignKey("CoberturaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminPolizasAPI.Entidades.Poliza", "Poliza")
                        .WithMany()
                        .HasForeignKey("PolizaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cobertura");

                    b.Navigation("Poliza");
                });
#pragma warning restore 612, 618
        }
    }
}
