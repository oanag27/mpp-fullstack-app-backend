﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mmp_prj.Models;

#nullable disable

namespace mmp_prj.Migrations
{
    [DbContext(typeof(MppContext))]
    [Migration("20240520083327_AddTableUserModelsId")]
    partial class AddTableUserModelsId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("mmp_prj.Models.Subtask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Completed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("completed");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int")
                        .HasColumnName("task_id");

                    b.HasKey("Id")
                        .HasName("PK__Subtask__3214EC0758C1D9C3");

                    b.HasIndex("TaskId");

                    b.ToTable("Subtask", (string)null);
                });

            modelBuilder.Entity("mmp_prj.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Tasks__3214EC07C3EA6D98");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("mmp_prj.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserModels");
                });

            modelBuilder.Entity("mmp_prj.Models.Subtask", b =>
                {
                    b.HasOne("mmp_prj.Models.Task", "Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK__Subtask__task_id__3A81B327");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("mmp_prj.Models.Task", b =>
                {
                    b.Navigation("Subtasks");
                });
#pragma warning restore 612, 618
        }
    }
}
