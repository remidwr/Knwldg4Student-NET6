﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(KnwldgContext))]
    partial class KnwldgContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("French_CI_AI")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("CoursesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("Domain.AggregatesModel.MeetingAggregate.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MeetingStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("MeetingStatusId");

                    b.ToTable("Meetings", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.MeetingAggregate.MeetingStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("MeetingStatus", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.SectionAggregate.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex(new[] { "Label" }, "UK_Courses_Label")
                        .IsUnique();

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.SectionAggregate.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Title" }, "UK_Sections_Title")
                        .IsUnique();

                    b.ToTable("Sections", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Stars")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(2,1)")
                        .HasDefaultValue(5m);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Ratings", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "LastName", "FirstName" }, "IX_Students_FullName");

                    b.HasIndex(new[] { "Email" }, "UK_Students_Email")
                        .IsUnique();

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.StudentMeeting", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<bool>("HasConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInstructor")
                        .HasColumnType("bit");

                    b.HasKey("StudentId", "MeetingId");

                    b.HasIndex("MeetingId");

                    b.ToTable("StudentMeetings", (string)null);
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.UnavailableDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DayOff")
                        .HasColumnType("date");

                    b.Property<int>("Interval")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("Recursive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex(new[] { "DayOff", "StudentId" }, "UK_Table_DayOff_StudentId")
                        .IsUnique();

                    b.ToTable("UnavailableDays", (string)null);
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("Domain.AggregatesModel.SectionAggregate.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.AggregatesModel.StudentAggregate.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.AggregatesModel.MeetingAggregate.Meeting", b =>
                {
                    b.HasOne("Domain.AggregatesModel.SectionAggregate.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.AggregatesModel.MeetingAggregate.MeetingStatus", "MeetingStatus")
                        .WithMany()
                        .HasForeignKey("MeetingStatusId")
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("MeetingStatus");
                });

            modelBuilder.Entity("Domain.AggregatesModel.SectionAggregate.Course", b =>
                {
                    b.HasOne("Domain.AggregatesModel.SectionAggregate.Section", "Section")
                        .WithMany("Courses")
                        .HasForeignKey("SectionId")
                        .IsRequired()
                        .HasConstraintName("FK_Courses_Sections");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.Rating", b =>
                {
                    b.HasOne("Domain.AggregatesModel.StudentAggregate.Student", "Student")
                        .WithMany("Ratings")
                        .HasForeignKey("StudentId")
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.StudentMeeting", b =>
                {
                    b.HasOne("Domain.AggregatesModel.MeetingAggregate.Meeting", "Meeting")
                        .WithMany("StudentMeetings")
                        .HasForeignKey("MeetingId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentMeetings_Meetings");

                    b.HasOne("Domain.AggregatesModel.StudentAggregate.Student", "Student")
                        .WithMany("StudentMeetings")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentMeetings_Students");

                    b.Navigation("Meeting");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.UnavailableDay", b =>
                {
                    b.HasOne("Domain.AggregatesModel.StudentAggregate.Student", "Student")
                        .WithMany("UnavailableDays")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_UnavailableDays_Students");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Domain.AggregatesModel.MeetingAggregate.Meeting", b =>
                {
                    b.Navigation("StudentMeetings");
                });

            modelBuilder.Entity("Domain.AggregatesModel.SectionAggregate.Section", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Domain.AggregatesModel.StudentAggregate.Student", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("StudentMeetings");

                    b.Navigation("UnavailableDays");
                });
#pragma warning restore 612, 618
        }
    }
}
