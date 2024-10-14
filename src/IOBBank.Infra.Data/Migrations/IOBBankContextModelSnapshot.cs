﻿// <auto-generated />
using System;
using IOBBank.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IOBBank.Infra.Data.Migrations
{
    [DbContext(typeof(IOBBankContext))]
    partial class IOBBankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IOBBank.Domain.Entidades.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Account")
                        .HasColumnType("int");

                    b.Property<double>("Balance")
                        .HasColumnType("double");

                    b.Property<int>("Branch")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("BankAccount", "public");
                });

            modelBuilder.Entity("IOBBank.Domain.Entidades.BankLaunch", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("DestinationBankAccountId")
                        .HasColumnType("char(36)");

                    b.Property<int>("OperationType")
                        .HasColumnType("int");

                    b.Property<Guid>("OriginBankAccountId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Value")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("DestinationBankAccountId");

                    b.ToTable("BankLaunch", "public");
                });

            modelBuilder.Entity("IOBBank.Domain.Entidades.BankLaunch", b =>
                {
                    b.HasOne("IOBBank.Domain.Entidades.BankAccount", "BankAccount")
                        .WithMany()
                        .HasForeignKey("DestinationBankAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BankAccount");
                });
#pragma warning restore 612, 618
        }
    }
}