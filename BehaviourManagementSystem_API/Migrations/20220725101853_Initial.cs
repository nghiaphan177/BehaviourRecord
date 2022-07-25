﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BehaviourManagementSystem_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyzeAntecedentActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzeAntecedentActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyzeAntecedentEnvironmentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzeAntecedentEnvironmentals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyzeAntecedentPerceives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzeAntecedentPerceives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileExtremes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileExtremes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMilds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileModerates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileModerates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileRecoveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileRecoveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvtName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<bool>(type: "bit", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "date", nullable: true),
                    RecordStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordDuring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordWhere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordWho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeBehaviour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeAntecedentPerceivedDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeAntecedentEnvironmentalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeAntecedentActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeConsequencesPerceive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeConsequenceEnvironmental = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalyzeConsequencesActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionAntecedent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionConsequece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordIsCompeleted = table.Column<bool>(type: "bit", nullable: true),
                    AnalyzeIsCompeleted = table.Column<bool>(type: "bit", nullable: true),
                    FunctionIsCompeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IndividualId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnalyzeAntecedentPerceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnalyzeAntecedentEnvironmentalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AnalyzeAntecedentActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_AnalyzeAntecedentActivities_AnalyzeAntecedentActivityId",
                        column: x => x.AnalyzeAntecedentActivityId,
                        principalTable: "AnalyzeAntecedentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assessments_AnalyzeAntecedentEnvironmentals_AnalyzeAntecedentEnvironmentalId",
                        column: x => x.AnalyzeAntecedentEnvironmentalId,
                        principalTable: "AnalyzeAntecedentEnvironmentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assessments_AnalyzeAntecedentPerceives_AnalyzeAntecedentPerceivedId",
                        column: x => x.AnalyzeAntecedentPerceivedId,
                        principalTable: "AnalyzeAntecedentPerceives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assessments_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Interventions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileDate = table.Column<DateTime>(type: "date", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileMildDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileModerateDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileExtremeDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileRecoveryDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManageMild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManageModerate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManageExtreme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManageRecovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventInteraction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventInvironmental = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileIsCompeleted = table.Column<bool>(type: "bit", nullable: true),
                    ManageIsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    PreventIsCompleted = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssesetmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileMildId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileModerateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileExtremeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfileRecoveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interventions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interventions_Assessments_AssesetmentId",
                        column: x => x.AssesetmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Interventions_ProfileExtremes_ProfileExtremeId",
                        column: x => x.ProfileExtremeId,
                        principalTable: "ProfileExtremes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Interventions_ProfileMilds_ProfileMildId",
                        column: x => x.ProfileMildId,
                        principalTable: "ProfileMilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Interventions_ProfileModerates_ProfileModerateId",
                        column: x => x.ProfileModerateId,
                        principalTable: "ProfileModerates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Interventions_ProfileRecoveries_ProfileRecoveryId",
                        column: x => x.ProfileRecoveryId,
                        principalTable: "ProfileRecoveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("60002d7c-e4e0-468a-9b5e-f5d76246967f"), "6AE595FF-9975-445F-8A6F-271E67C0A40B", "admin", "ADMIN" },
                    { new Guid("033fae0a-dac5-470f-9d25-14aac8ac7576"), "BC35628A-4FC2-4A5F-8C04-387DDE83229C", "teacher", "TEACHER" },
                    { new Guid("4dd7b2b7-72a0-4c0f-b6eb-06639897a8fb"), "59FB9FCA-7A8D-412D-99A6-3FC3F42C0B2D", "student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Activity", "ActivityDate", "Address", "AvtName", "ConcurrencyStamp", "CreateDate", "DOB", "DisplayName", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdateDate", "UserName" },
                values: new object[] { new Guid("15746e42-b7f2-44ff-a38c-7e4d6aa84b19"), 0, true, new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "default_avt.png", "733CF691-8A0A-4EA2-BA83-688824D9BA83", null, new DateTime(1998, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "lhduy3011@gmail.com", true, "Lê", "Nam", "Hoàng Duy", false, null, "LHDUY3011@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEHFw/r2f6mIiTh+trGovd4+58ChYS2VNaum7jCI0RVleUNJdYtj0cfRDrq1EKj0+3Q==", "0334102197", true, "733cf691-8a0a-4ea2-ba83-688824d9ba83", false, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("60002d7c-e4e0-468a-9b5e-f5d76246967f"), new Guid("15746e42-b7f2-44ff-a38c-7e4d6aa84b19") });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AnalyzeAntecedentActivityId",
                table: "Assessments",
                column: "AnalyzeAntecedentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AnalyzeAntecedentEnvironmentalId",
                table: "Assessments",
                column: "AnalyzeAntecedentEnvironmentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_AnalyzeAntecedentPerceivedId",
                table: "Assessments",
                column: "AnalyzeAntecedentPerceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_IndividualId",
                table: "Assessments",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_StudentId",
                table: "Individuals",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_AssesetmentId",
                table: "Interventions",
                column: "AssesetmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ProfileExtremeId",
                table: "Interventions",
                column: "ProfileExtremeId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ProfileMildId",
                table: "Interventions",
                column: "ProfileMildId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ProfileModerateId",
                table: "Interventions",
                column: "ProfileModerateId");

            migrationBuilder.CreateIndex(
                name: "IX_Interventions_ProfileRecoveryId",
                table: "Interventions",
                column: "ProfileRecoveryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interventions");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "ProfileExtremes");

            migrationBuilder.DropTable(
                name: "ProfileMilds");

            migrationBuilder.DropTable(
                name: "ProfileModerates");

            migrationBuilder.DropTable(
                name: "ProfileRecoveries");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AnalyzeAntecedentActivities");

            migrationBuilder.DropTable(
                name: "AnalyzeAntecedentEnvironmentals");

            migrationBuilder.DropTable(
                name: "AnalyzeAntecedentPerceives");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
