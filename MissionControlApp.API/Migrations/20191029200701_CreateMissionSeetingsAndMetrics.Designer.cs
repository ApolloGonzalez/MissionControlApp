﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionControlApp.API.Data;

namespace MissionControlApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191029200701_CreateMissionSeetingsAndMetrics")]
    partial class CreateMissionSeetingsAndMetrics
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Accelerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcceleratorName");

                    b.Property<bool>("Active");

                    b.Property<int>("BusinessFunctionId");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int>("IndustryId");

                    b.Property<string>("ModelType");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFunctionId");

                    b.HasIndex("IndustryId");

                    b.ToTable("Accelerators");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.BusinessFunction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("BusinessFunctionAlias");

                    b.Property<string>("BusinessFunctionName");

                    b.Property<DateTime?>("DateCreated");

                    b.HasKey("Id");

                    b.ToTable("BusinessFunctions");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Industry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("IndustryAlias");

                    b.Property<string>("IndustryName");

                    b.HasKey("Id");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Like", b =>
                {
                    b.Property<int>("LikerId");

                    b.Property<int>("LikeeId");

                    b.HasKey("LikerId", "LikeeId");

                    b.HasIndex("LikeeId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime?>("DateRead");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime>("MessageSent");

                    b.Property<bool>("RecipientDeleted");

                    b.Property<int>("RecipientId");

                    b.Property<bool>("SenderDeleted");

                    b.Property<int>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal>("ActualCost")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<decimal>("ActualRoi")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<int>("BusinessFunctionId");

                    b.Property<string>("BusinessImpact");

                    b.Property<string>("Challenge");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("DesiredOutcome");

                    b.Property<decimal>("EstimatedRoi")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<int>("IndustryId");

                    b.Property<string>("MissionName");

                    b.Property<int>("MissionStatusId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(2);

                    b.Property<bool>("Public");

                    b.Property<int>("TimeFrame");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BusinessFunctionId");

                    b.HasIndex("IndustryId");

                    b.HasIndex("MissionStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAccelerator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AcceleratorId");

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<int>("MissionId");

                    b.HasKey("Id");

                    b.HasIndex("AcceleratorId");

                    b.HasIndex("MissionId");

                    b.ToTable("MissionAccelerators");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAppSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<int>("MissionId");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("MissionId")
                        .IsUnique();

                    b.ToTable("MissionAppSettings");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAssessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccuracyRequirement");

                    b.Property<bool>("Active");

                    b.Property<string>("ChallengeSolution");

                    b.Property<string>("ChallengeType");

                    b.Property<string>("DataDomainExpert");

                    b.Property<string>("DataLocation");

                    b.Property<string>("DataQuality");

                    b.Property<string>("DataType");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<decimal>("EstimatedCost")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("InfrastructureRequirement");

                    b.Property<int>("MissionId");

                    b.Property<DateTime?>("UpdateDateTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MissionId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("MissionAssessments");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionMetrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal>("Cost")
                        .HasColumnType("Money");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<int>("ExperimentRuns");

                    b.Property<int>("Experiments");

                    b.Property<int>("Ideas");

                    b.Property<int>("Mission");

                    b.Property<int>("MissionId");

                    b.Property<string>("MissionStatus");

                    b.Property<int>("Models");

                    b.Property<int>("ModelsDeployed");

                    b.Property<int>("NotebookVMs");

                    b.Property<DateTime?>("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("MissionId")
                        .IsUnique();

                    b.ToTable("MissionMetrics");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionPlatform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<int>("MissionId");

                    b.Property<int>("PlatformId");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.HasIndex("PlatformId");

                    b.ToTable("MissionPlatforms");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("MissionStatusAlias");

                    b.Property<string>("MissionStatusCode");

                    b.Property<string>("MissionStatusName");

                    b.Property<string>("MissionStatusType");

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.ToTable("MissionStatus");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<int>("MissionId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.HasIndex("UserId");

                    b.ToTable("MissionTeams");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("PlatformAlias");

                    b.Property<string>("PlatformName");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("Employee");

                    b.Property<string>("Experience");

                    b.Property<string>("Gender");

                    b.Property<string>("Interests");

                    b.Property<string>("Introduction");

                    b.Property<string>("JobTitle");

                    b.Property<string>("KnownAs");

                    b.Property<DateTime>("LastActive");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MissionControlApp.API.Models.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Accelerator", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.BusinessFunction", "BusinessFunction")
                        .WithMany()
                        .HasForeignKey("BusinessFunctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Like", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User", "Likee")
                        .WithMany("Likers")
                        .HasForeignKey("LikeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MissionControlApp.API.Models.User", "Liker")
                        .WithMany("Likees")
                        .HasForeignKey("LikerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Message", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MissionControlApp.API.Models.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Mission", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.BusinessFunction", "BusinessFunction")
                        .WithMany()
                        .HasForeignKey("BusinessFunctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.MissionStatus", "MissionStatus")
                        .WithMany()
                        .HasForeignKey("MissionStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.User", "User")
                        .WithMany("Missions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAccelerator", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Accelerator", "Accelerator")
                        .WithMany()
                        .HasForeignKey("AcceleratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.Mission", "Mission")
                        .WithMany("MissionAccelerators")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAppSettings", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Mission", "Mission")
                        .WithOne("MissionAppSettings")
                        .HasForeignKey("MissionControlApp.API.Models.MissionAppSettings", "MissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionAssessment", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Mission", "Mission")
                        .WithOne("MissionAssessment")
                        .HasForeignKey("MissionControlApp.API.Models.MissionAssessment", "MissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MissionControlApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionMetrics", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Mission")
                        .WithOne("MissionMetrics")
                        .HasForeignKey("MissionControlApp.API.Models.MissionMetrics", "MissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionPlatform", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Mission", "Mission")
                        .WithMany("MissionPlatforms")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MissionControlApp.API.Models.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.MissionTeam", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Mission", "Mission")
                        .WithMany("MissionTeam")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MissionControlApp.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.Photo", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MissionControlApp.API.Models.UserRole", b =>
                {
                    b.HasOne("MissionControlApp.API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MissionControlApp.API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
