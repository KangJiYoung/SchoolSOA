﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolSOA.Services.Blog.Entities;

namespace Blog.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20190110120116_SeedBlogs")]
    partial class SeedBlogs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolSOA.Services.Blog.Entities.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid>("CreatorId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Blogs");

                    b.HasData(
                        new { Id = new Guid("e9f1ad0b-e281-44db-b4fc-c3c2a23919ff"), Content = "Content-1", CreatorId = new Guid("7762e7eb-d979-499c-882d-bbe31e2bfc15"), Title = "Title-1" },
                        new { Id = new Guid("d8d603ae-b486-4d6a-92ab-5f7bb87c6495"), Content = "Content-2", CreatorId = new Guid("6b072a48-9223-4e78-8269-5563015129ea"), Title = "Title-2" },
                        new { Id = new Guid("3f4cf919-884c-418b-bea3-bd400ace37fe"), Content = "Content-3", CreatorId = new Guid("91b66212-7a01-4c13-bfe6-ba196d414fe0"), Title = "Title-3" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
