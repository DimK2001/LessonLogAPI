﻿// <auto-generated />
using System;
using LessonLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LessonLog.Infrastructure.Migrations
{
    [DbContext(typeof(LessonLogContext))]
    [Migration("20211121172147_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LessonLog.Domain.Attendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Late")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Leaving")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("PresencePercentage")
                        .HasColumnType("real");

                    b.Property<DateTime>("PresenceTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendences");
                });

            modelBuilder.Entity("LessonLog.Domain.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DirectionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("LessonLog.Domain.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Theme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("LessonLog.Domain.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AttestationMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamMark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GroupFlag")
                        .HasColumnType("bit");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdManagementSys")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LessonLog.Domain.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("LessonLog.Domain.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AcademicTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LessinId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Post")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("LessonLog.Domain.Сlassroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonId")
                        .IsUnique();

                    b.ToTable("Сlassrooms");
                });

            modelBuilder.Entity("LessonLog.Domain.Attendance", b =>
                {
                    b.HasOne("LessonLog.Domain.Lesson", "Lesson")
                        .WithMany("Attendances")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LessonLog.Domain.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LessonLog.Domain.Group", b =>
                {
                    b.HasOne("LessonLog.Domain.Lesson", "Lesson")
                        .WithMany("Groups")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LessonLog.Domain.Lesson", b =>
                {
                    b.HasOne("LessonLog.Domain.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("LessonLog.Domain.Student", b =>
                {
                    b.HasOne("LessonLog.Domain.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("LessonLog.Domain.Teacher", b =>
                {
                    b.HasOne("LessonLog.Domain.Lesson", "Lesson")
                        .WithMany("Teachers")
                        .HasForeignKey("LessonId");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LessonLog.Domain.Сlassroom", b =>
                {
                    b.HasOne("LessonLog.Domain.Lesson", "Lesson")
                        .WithOne("Сlassroom")
                        .HasForeignKey("LessonLog.Domain.Сlassroom", "LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("LessonLog.Domain.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("LessonLog.Domain.Lesson", b =>
                {
                    b.Navigation("Сlassroom");

                    b.Navigation("Attendances");

                    b.Navigation("Groups");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("LessonLog.Domain.Student", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("LessonLog.Domain.Subject", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
