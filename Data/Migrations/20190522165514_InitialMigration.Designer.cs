﻿// <auto-generated />
using System;
using Geneao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Geneao.Data.Migrations
{
    [DbContext(typeof(GeneaoDbContext))]
    [Migration("20190522165514_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Geneao.Data.Models.Famille", b =>
                {
                    b.Property<string>("Nom")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Famille_Id");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Deleted")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DeletionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DeleteDate")
                        .HasDefaultValue(null);

                    b.Property<DateTime>("EditDate")
                        .HasColumnName("EditDate");

                    b.HasKey("Nom");

                    b.ToTable("Famille");
                });
#pragma warning restore 612, 618
        }
    }
}
