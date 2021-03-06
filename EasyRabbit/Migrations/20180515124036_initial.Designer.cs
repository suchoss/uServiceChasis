﻿// <auto-generated />
using EasyRabbit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace EasyRabbit.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180515124036_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyRabbit.Models.Calculation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FirstNumber");

                    b.Property<int>("Result");

                    b.Property<int>("SecondNumber");

                    b.HasKey("Id");

                    b.ToTable("Calculations");
                });
#pragma warning restore 612, 618
        }
    }
}
