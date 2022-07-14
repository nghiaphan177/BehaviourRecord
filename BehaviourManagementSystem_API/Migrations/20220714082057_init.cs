using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BehaviourManagementSystem_API.Migrations
{
    public partial class init : Migration
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
                name: "TermConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Activity = table.Column<bool>(type: "bit", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    RecordIncident = table.Column<int>(type: "int", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentHTML = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                name: "Assesetments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordStart = table.Column<TimeSpan>(type: "time", nullable: true),
                    RecordEnd = table.Column<TimeSpan>(type: "time", nullable: true),
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
                    table.PrimaryKey("PK_Assesetments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assesetments_AnalyzeAntecedentActivities_AnalyzeAntecedentActivityId",
                        column: x => x.AnalyzeAntecedentActivityId,
                        principalTable: "AnalyzeAntecedentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assesetments_AnalyzeAntecedentEnvironmentals_AnalyzeAntecedentEnvironmentalId",
                        column: x => x.AnalyzeAntecedentEnvironmentalId,
                        principalTable: "AnalyzeAntecedentEnvironmentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assesetments_AnalyzeAntecedentPerceives_AnalyzeAntecedentPerceivedId",
                        column: x => x.AnalyzeAntecedentPerceivedId,
                        principalTable: "AnalyzeAntecedentPerceives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Assesetments_Individuals_IndividualId",
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
                    ProfileDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_Interventions_Assesetments_AssesetmentId",
                        column: x => x.AssesetmentId,
                        principalTable: "Assesetments",
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

            migrationBuilder.CreateIndex(
                name: "IX_Assesetments_AnalyzeAntecedentActivityId",
                table: "Assesetments",
                column: "AnalyzeAntecedentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Assesetments_AnalyzeAntecedentEnvironmentalId",
                table: "Assesetments",
                column: "AnalyzeAntecedentEnvironmentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Assesetments_AnalyzeAntecedentPerceivedId",
                table: "Assesetments",
                column: "AnalyzeAntecedentPerceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_Assesetments_IndividualId",
                table: "Assesetments",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_UserId",
                table: "Individuals",
                column: "UserId");

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
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

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
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "TermConditions");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Assesetments");

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
