﻿// <auto-generated />
using IncludeTypeBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IncludeTypeBackend.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    partial class PostgreSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IncludeTypeBackend.Models.Privacy", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Companies")
                        .HasColumnType("text");

                    b.Property<string>("Contact")
                        .HasColumnType("text");

                    b.Property<string>("Education")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Experience")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<string>("Projects")
                        .HasColumnType("text");

                    b.Property<string>("Skills")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Privacy", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.ProfessionalProfile", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Companies")
                        .HasColumnType("text");

                    b.Property<string>("Education")
                        .HasColumnType("text");

                    b.Property<int>("ExperienceMonths")
                        .HasColumnType("integer");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("integer");

                    b.Property<string>("Projects")
                        .HasColumnType("text");

                    b.Property<string>("Skills")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("ProfessionalProfile", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Documentation")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Project", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.ProjectIssue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Assigned")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Deadline")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .HasColumnType("text");

                    b.Property<string>("ProjId")
                        .HasColumnType("text");

                    b.Property<string>("ProjName")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProjectIssue", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.ProjectMember", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ProjName")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProjectMember", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.ProjectTask", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Assigned")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .HasColumnType("text");

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("Date")
                        .HasColumnType("text");

                    b.Property<string>("Deadline")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .HasColumnType("text");

                    b.Property<string>("ProjId")
                        .HasColumnType("text");

                    b.Property<string>("ProjName")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProjectTask", "public");
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Contact")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<string>("Pincode")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User", "public");

                    b.HasData(
                        new
                        {
                            Id = "daedeedc-2adf-41fd-ba48-cd6c8e5097ce",
                            Address = "",
                            Bio = "",
                            City = "",
                            Contact = "",
                            Country = "",
                            Email = "subhamkarmakar0901@gmail.com",
                            FirstName = "Subham",
                            IsAdmin = true,
                            LastName = "Karmakar",
                            Password = "$2a$11$MtHAr1tKWFl39PdDlw5rjO2oUS0nQMwTtZq/LDjeu3W6/u9GhF2Bu",
                            Picture = "",
                            Pincode = "",
                            State = "",
                            Username = "SubhamK108"
                        });
                });

            modelBuilder.Entity("IncludeTypeBackend.Models.UserVerification", b =>
                {
                    b.Property<string>("UniqueString")
                        .HasColumnType("text");

                    b.Property<string>("CreationTime")
                        .HasColumnType("text");

                    b.Property<string>("ExpirationTime")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("UniqueString");

                    b.ToTable("UserVerification", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
