using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsProfessional = table.Column<bool>(type: "bit", nullable: false),
                    IsClient = table.Column<bool>(type: "bit", nullable: false),
                    ProfessionalProfileId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalProfile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "BIGINT", nullable: false),
                    ProfessionalName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    Expertise = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    AcademicRegistry = table.Column<string>(type: "NVARCHAR(15)", maxLength: 15, nullable: false),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    TeachingInstitution = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    IsPremium = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    SkillDolarBalance = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOffered",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: true),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    ProfessionalProfileId = table.Column<long>(type: "BIGINT", nullable: false),
                    CategoryId = table.Column<long>(type: "BIGINT", nullable: false),
                    CategoryId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceOffered_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceOffered_Category_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceOffered_ProfessionalProfile_ProfessionalProfileId",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<long>(type: "BIGINT", nullable: false),
                    ReviewerId = table.Column<long>(type: "BIGINT", nullable: false),
                    Rating = table.Column<int>(type: "INT", nullable: false),
                    Comment = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    ProfessionalProfileId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_ProfessionalProfile_ProfessionalProfileId",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_ServiceOffered_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkillSwapRequest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessionalAId = table.Column<long>(type: "BIGINT", nullable: false),
                    ProfessionalBId = table.Column<long>(type: "BIGINT", nullable: false),
                    ServiceAId = table.Column<long>(type: "BIGINT", nullable: true),
                    ServiceBId = table.Column<long>(type: "BIGINT", nullable: false),
                    SwapDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    Status = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    OfferedAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillSwapRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillSwapRequest_ProfessionalProfile_ProfessionalAId",
                        column: x => x.ProfessionalAId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillSwapRequest_ProfessionalProfile_ProfessionalBId",
                        column: x => x.ProfessionalBId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillSwapRequest_ServiceOffered_ServiceAId",
                        column: x => x.ServiceAId,
                        principalTable: "ServiceOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SkillSwapRequest_ServiceOffered_ServiceBId",
                        column: x => x.ServiceBId,
                        principalTable: "ServiceOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(type: "BIGINT", nullable: false),
                    ServiceId = table.Column<long>(type: "BIGINT", nullable: false),
                    ServiceTitle = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    RequestDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    Observations = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false, defaultValue: "Pendente"),
                    ReviewId = table.Column<long>(type: "bigint", nullable: true),
                    ApplicationUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequest_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceRequest_ServiceOffered_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<long>(type: "BIGINT", nullable: false),
                    ReceiverId = table.Column<long>(type: "BIGINT", nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    SentAt = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    IsRead = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    SkillSwapRequestId = table.Column<long>(type: "BIGINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_SkillSwapRequest_SkillSwapRequestId",
                        column: x => x.SkillSwapRequestId,
                        principalTable: "SkillSwapRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderProfileId = table.Column<long>(type: "BIGINT", nullable: false),
                    ReceiverProfileId = table.Column<long>(type: "BIGINT", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(1000)", maxLength: 1000, nullable: false),
                    SkillSwapRequestId = table.Column<long>(type: "BIGINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_ProfessionalProfile_ReceiverProfileId",
                        column: x => x.ReceiverProfileId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_ProfessionalProfile_SenderProfileId",
                        column: x => x.SenderProfileId,
                        principalTable: "ProfessionalProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_SkillSwapRequest_SkillSwapRequestId",
                        column: x => x.SkillSwapRequestId,
                        principalTable: "SkillSwapRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfessionalProfileId",
                table: "AspNetUsers",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ReceiverId",
                table: "Message",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SkillSwapRequestId",
                table: "Message",
                column: "SkillSwapRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalProfile_UserId",
                table: "ProfessionalProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProfessionalProfileId",
                table: "Review",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewerId",
                table: "Review",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ServiceId",
                table: "Review",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffered_CategoryId",
                table: "ServiceOffered",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffered_CategoryId1",
                table: "ServiceOffered",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffered_ProfessionalProfileId",
                table: "ServiceOffered",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ApplicationUserId",
                table: "ServiceRequest",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ClientId",
                table: "ServiceRequest",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ReviewId",
                table: "ServiceRequest",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceId",
                table: "ServiceRequest",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSwapRequest_ProfessionalAId",
                table: "SkillSwapRequest",
                column: "ProfessionalAId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSwapRequest_ProfessionalBId",
                table: "SkillSwapRequest",
                column: "ProfessionalBId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSwapRequest_ServiceAId",
                table: "SkillSwapRequest",
                column: "ServiceAId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillSwapRequest_ServiceBId",
                table: "SkillSwapRequest",
                column: "ServiceBId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ReceiverProfileId",
                table: "Transaction",
                column: "ReceiverProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SenderProfileId",
                table: "Transaction",
                column: "SenderProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_SkillSwapRequestId",
                table: "Transaction",
                column: "SkillSwapRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfessionalProfile_ProfessionalProfileId",
                table: "AspNetUsers",
                column: "ProfessionalProfileId",
                principalTable: "ProfessionalProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalProfile_AspNetUsers_UserId",
                table: "ProfessionalProfile");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "ServiceRequest");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SkillSwapRequest");

            migrationBuilder.DropTable(
                name: "ServiceOffered");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProfessionalProfile");
        }
    }
}
