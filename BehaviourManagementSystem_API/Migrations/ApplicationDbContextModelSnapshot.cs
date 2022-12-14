// <auto-generated />
using System;
using BehaviourManagementSystem_API.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BehaviourManagementSystem_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AnalyzeAntecedentActivities");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentEnvironmental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AnalyzeAntecedentEnvironmentals");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentPerceive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AnalyzeAntecedentPerceives");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Assessment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnalyzeAntecedentActivityDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AnalyzeAntecedentActivityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnalyzeAntecedentEnvironmentalDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AnalyzeAntecedentEnvironmentalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnalyzeAntecedentPerceivedDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("AnalyzeAntecedentPerceivedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnalyzeBehaviour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnalyzeConsequenceEnvironmental")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnalyzeConsequencesActivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnalyzeConsequencesPerceive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("AnalyzeIsCompeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FunctionAntecedent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FunctionConsequece")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("FunctionIsCompeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IndividualId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RecordDate")
                        .HasColumnType("date");

                    b.Property<string>("RecordDuring")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordEnd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("RecordIsCompeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RecordStart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordWhere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordWho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AnalyzeAntecedentActivityId");

                    b.HasIndex("AnalyzeAntecedentEnvironmentalId");

                    b.HasIndex("AnalyzeAntecedentPerceivedId");

                    b.HasIndex("IndividualId");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Individual", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Individuals");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Intervention", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssesetmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ManageExtreme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("ManageIsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("ManageMild")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManageModerate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManageRecovery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventActivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventInteraction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreventInvironmental")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("PreventIsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("PreventStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProfileDate")
                        .HasColumnType("date");

                    b.Property<string>("ProfileExtremeDesciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileExtremeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("ProfileIsCompeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileMildDesciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileMildId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProfileModerateDesciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileModerateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProfileRecoveryDesciption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileRecoveryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AssesetmentId");

                    b.HasIndex("ProfileExtremeId");

                    b.HasIndex("ProfileMildId");

                    b.HasIndex("ProfileModerateId");

                    b.HasIndex("ProfileRecoveryId");

                    b.ToTable("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileExtreme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProfileExtremes");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileMild", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProfileMilds");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileModerate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProfileModerates");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileRecovery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProfileRecoveries");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b2e798e5-72da-4aea-b61c-cf5abaa98324"),
                            ConcurrencyStamp = "A1413198-E017-4301-BD50-81C4164075D6",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("4c3ebe08-54de-46e9-98dd-3125a0a34153"),
                            ConcurrencyStamp = "355C8B1D-A04E-4022-B245-57100EEC27A2",
                            Name = "teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = new Guid("ffa03385-9ad9-4fca-b74d-044640074e45"),
                            ConcurrencyStamp = "6AE6D72F-6184-458A-BC9F-0370583AA1BD",
                            Name = "student",
                            NormalizedName = "STUDENT"
                        });
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Activity")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ActivityDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvtName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("date");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ff83058c-a8a3-4792-9324-ef4d6d08f3db"),
                            AccessFailedCount = 0,
                            Activity = true,
                            ActivityDate = new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            AvtName = "default_avt.png",
                            ConcurrencyStamp = "C450D099-0E9B-498C-B4DD-08C58026F48C",
                            DOB = new DateTime(1998, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lhduy12cb34@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Lê",
                            Gender = "Nam",
                            LastName = "Hoàng Duy",
                            LockoutEnabled = false,
                            NormalizedEmail = "LHDUY12CB34@GMAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEFcefCrVqY8xLwRcAHCsdu0MCf82K098P0rnHAFWJ2O7ZFpske8kc+eJ/QRFRhV5qg==",
                            PhoneNumber = "0779863007",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "c450d099-0e9b-498c-b4dd-08c58026f48c",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("ff83058c-a8a3-4792-9324-ef4d6d08f3db"),
                            RoleId = new Guid("b2e798e5-72da-4aea-b61c-cf5abaa98324")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Assessment", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.AnalyzeAntecedentActivity", "AnalyzeAntecedentActivity")
                        .WithMany("Assesetments")
                        .HasForeignKey("AnalyzeAntecedentActivityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.AnalyzeAntecedentEnvironmental", "AnalyzeAntecedentEnvironmental")
                        .WithMany("Assesetments")
                        .HasForeignKey("AnalyzeAntecedentEnvironmentalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.AnalyzeAntecedentPerceive", "AnalyzeAntecedentPerceive")
                        .WithMany("Assesetments")
                        .HasForeignKey("AnalyzeAntecedentPerceivedId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.Individual", "Individual")
                        .WithMany("Assesetments")
                        .HasForeignKey("IndividualId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AnalyzeAntecedentActivity");

                    b.Navigation("AnalyzeAntecedentEnvironmental");

                    b.Navigation("AnalyzeAntecedentPerceive");

                    b.Navigation("Individual");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Individual", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.User", "User")
                        .WithMany("Individuals")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Intervention", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.Assessment", "Assesetment")
                        .WithMany("Interventions")
                        .HasForeignKey("AssesetmentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.ProfileExtreme", "ProfileExtreme")
                        .WithMany("Interventions")
                        .HasForeignKey("ProfileExtremeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.ProfileMild", "ProfileMild")
                        .WithMany("Interventions")
                        .HasForeignKey("ProfileMildId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.ProfileModerate", "ProfileModerate")
                        .WithMany("Interventions")
                        .HasForeignKey("ProfileModerateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("BehaviourManagementSystem_API.Models.ProfileRecovery", "ProfileRecovery")
                        .WithMany("Interventions")
                        .HasForeignKey("ProfileRecoveryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Assesetment");

                    b.Navigation("ProfileExtreme");

                    b.Navigation("ProfileMild");

                    b.Navigation("ProfileModerate");

                    b.Navigation("ProfileRecovery");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BehaviourManagementSystem_API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BehaviourManagementSystem_API.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentActivity", b =>
                {
                    b.Navigation("Assesetments");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentEnvironmental", b =>
                {
                    b.Navigation("Assesetments");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.AnalyzeAntecedentPerceive", b =>
                {
                    b.Navigation("Assesetments");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Assessment", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.Individual", b =>
                {
                    b.Navigation("Assesetments");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileExtreme", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileMild", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileModerate", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.ProfileRecovery", b =>
                {
                    b.Navigation("Interventions");
                });

            modelBuilder.Entity("BehaviourManagementSystem_API.Models.User", b =>
                {
                    b.Navigation("Individuals");
                });
#pragma warning restore 612, 618
        }
    }
}
