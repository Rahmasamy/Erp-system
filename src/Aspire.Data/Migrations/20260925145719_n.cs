using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aspire.Data.Migrations
{
    /// <inheritdoc />
    public partial class n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CreditBalance_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    DebitBalance_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    NetBalance_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    IsNature = table.Column<bool>(type: "boolean", nullable: true),
                    IsNetBalanceNature = table.Column<bool>(type: "boolean", nullable: true),
                    IsParentAccount = table.Column<bool>(type: "boolean", nullable: true),
                    IsReconciliationRequired = table.Column<bool>(type: "boolean", nullable: true),
                    IsPostingAccount = table.Column<bool>(type: "boolean", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADAddUserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADAddUserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADCreateAdmnAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdmnAccName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADCreateAdmnAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADCreateGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADCreateGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADCreateServAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServAcc = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADCreateServAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADCreateUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADCreateUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADDisableAdmnAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdmnAccName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDisableAdmnAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADDisableServAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServAcc = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDisableServAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADDisableUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDisableUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADEnableAdmnAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdmnAccName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADEnableAdmnAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADEnableServAccs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServAcc = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADEnableServAccs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADEnableUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADEnableUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADRmvUserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADRmvUserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADUpdateUserInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    EmailDisplayName = table.Column<string>(type: "text", nullable: true),
                    ExtNo = table.Column<string>(type: "text", nullable: true),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADUpdateUserInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADUpdateUserMngrs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NewManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADUpdateUserMngrs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ADUpdateUserMobls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NewMobile = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADUpdateUserMobls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsEtimad = table.Column<bool>(type: "boolean", nullable: true),
                    EtimadNo = table.Column<string>(type: "text", nullable: true),
                    BidRefNo = table.Column<string>(type: "text", nullable: true),
                    AwardLetterRefNo = table.Column<string>(type: "text", nullable: true),
                    ContractRefNo = table.Column<string>(type: "text", nullable: true),
                    PORefNo = table.Column<string>(type: "text", nullable: true),
                    IsLG = table.Column<bool>(type: "boolean", nullable: true),
                    LGRefNo = table.Column<string>(type: "text", nullable: true),
                    LGAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    LGExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDays_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true, defaultValue: "License"),
                    No = table.Column<string>(type: "text", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUserType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true),
                    LastAccessedTenant = table.Column<string>(type: "text", nullable: true),
                    SeatType = table.Column<string>(type: "text", nullable: true),
                    LangAbbrv = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    IsExternalUser = table.Column<bool>(type: "boolean", nullable: false),
                    OTP = table.Column<string>(type: "text", nullable: true),
                    IsValidProfile = table.Column<bool>(type: "boolean", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DomainId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    Serial = table.Column<string>(type: "text", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PurchaseValue_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentValue_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assumptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ProbabilityId = table.Column<long>(type: "bigint", nullable: true),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assumptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditItemControls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuditItemId = table.Column<long>(type: "bigint", nullable: true),
                    ControlId = table.Column<long>(type: "bigint", nullable: true),
                    ImplimentationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditItemControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditItemExceptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuditItemId = table.Column<long>(type: "bigint", nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditItemExceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    AuditId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Objective = table.Column<string>(type: "text", nullable: true),
                    Principal = table.Column<string>(type: "text", nullable: true),
                    Tools = table.Column<string>(type: "text", nullable: true),
                    Guidelines = table.Column<string>(type: "text", nullable: true),
                    Deliverables = table.Column<string>(type: "text", nullable: true),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    Ctrls = table.Column<int>(type: "integer", nullable: true),
                    Evids = table.Column<int>(type: "integer", nullable: true),
                    Excps = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    MinScoreLimit_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxScoreLimit_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AssessmentId = table.Column<long>(type: "bigint", nullable: true),
                    FrameworkId = table.Column<long>(type: "bigint", nullable: true),
                    AuditorId = table.Column<long>(type: "bigint", nullable: true),
                    ApproverId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: true),
                    IBAN = table.Column<string>(type: "text", nullable: true),
                    Balance_val = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    ShippingMethodId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RequiredDeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillingFrequencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillingPlans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "COCs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PhaseId = table.Column<long>(type: "bigint", nullable: true),
                    MilestoneId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CRNo = table.Column<string>(type: "text", nullable: true),
                    VATRegNo = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: true),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Confidences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confidences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constrains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constrains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssignedProjects = table.Column<string>(type: "text", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CRNo = table.Column<string>(type: "text", nullable: true),
                    VATRegNo = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: true),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    OldId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CI_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CI_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Abbrv = table.Column<string>(type: "text", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteAPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceAPId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteAPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditNoteARs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditNoteARs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Criticalities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criticalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExchangeRate = table.Column<decimal>(type: "numeric", nullable: true),
                    ExchangeRateTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsBaseCurrency = table.Column<bool>(type: "boolean", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPOs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<string>(type: "text", nullable: true),
                    ShippingMethodId = table.Column<string>(type: "text", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RequiredDeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    SalesOrderId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpeningBalance_val = table.Column<decimal>(type: "numeric", nullable: false),
                    IsBlock = table.Column<bool>(type: "boolean", nullable: false),
                    PriceListId = table.Column<long>(type: "bigint", nullable: true),
                    calc_PipelineAmount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Discount_prc = table.Column<double>(type: "double precision", nullable: true),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ManagementBuffer_prc = table.Column<double>(type: "double precision", nullable: true),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: true),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CRNo = table.Column<string>(type: "text", nullable: true),
                    VATRegNo = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: true),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataConfidentialities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataConfidentialities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DBLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    ActionUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DealLostReasons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealLostReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    PipelineId = table.Column<long>(type: "bigint", nullable: true),
                    StageId = table.Column<long>(type: "bigint", nullable: true),
                    ConfidenceId = table.Column<long>(type: "bigint", nullable: true),
                    IsEtimad = table.Column<bool>(type: "boolean", nullable: true),
                    EtimadNo = table.Column<string>(type: "text", nullable: true),
                    BidRefNo = table.Column<string>(type: "text", nullable: true),
                    AwardLetterRefNo = table.Column<string>(type: "text", nullable: true),
                    ContractRefNo = table.Column<string>(type: "text", nullable: true),
                    PORefNo = table.Column<string>(type: "text", nullable: true),
                    IsLG = table.Column<bool>(type: "boolean", nullable: true),
                    LGRefNo = table.Column<string>(type: "text", nullable: true),
                    LGValue_dec = table.Column<decimal>(type: "numeric", nullable: true),
                    LGExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDays_num = table.Column<int>(type: "integer", nullable: true),
                    calc_PipelineAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    TechnicalNotes = table.Column<string>(type: "text", nullable: true),
                    FinantialNotes = table.Column<string>(type: "text", nullable: true),
                    AccountNotes = table.Column<string>(type: "text", nullable: true),
                    LostNotes = table.Column<string>(type: "text", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AwardDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CompetitorWonAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CompetitorId = table.Column<long>(type: "bigint", nullable: true),
                    DealLostReasonId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebitNoteAPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceAPId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitNoteAPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DebitNoteARs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    SalesOrderId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitNoteARs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deliverables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PhaseId = table.Column<long>(type: "bigint", nullable: true),
                    MilestoneId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryProofs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProofs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    ProfitCenterId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineEmployees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisciplineUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMSProjectDisciplines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DMSProjectId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMSProjectDisciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMSProjectOwners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OldId = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CRNo = table.Column<string>(type: "text", nullable: true),
                    VATRegNo = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: true),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMSProjectOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMSProjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContractNo = table.Column<string>(type: "text", nullable: false),
                    ProjectCode = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    DMSProjectOwnerId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMSProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Prefix = table.Column<string>(type: "text", nullable: true),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    DMSProjectId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    OldWorkflowId = table.Column<long>(type: "bigint", nullable: true),
                    StatusDataMining = table.Column<string>(type: "text", nullable: true),
                    WorkflowDataMining = table.Column<string>(type: "text", nullable: true),
                    CurrentStatus = table.Column<string>(type: "text", nullable: true),
                    CurrentVersion = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentVersionFile = table.Column<string>(type: "text", nullable: true),
                    CurrentOldWorkflow = table.Column<string>(type: "text", nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: true),
                    IsUploadOnly = table.Column<bool>(type: "boolean", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
                    Dates = table.Column<string>(type: "text", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    PermanentDeleteEligibleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypeDisciplines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineUsrId = table.Column<long>(type: "bigint", nullable: true),
                    TaskName = table.Column<string>(type: "text", nullable: true),
                    Seq = table.Column<int>(type: "integer", nullable: true),
                    SLA_dec = table.Column<decimal>(type: "numeric", nullable: true),
                    Actions = table.Column<string>(type: "text", nullable: true),
                    Allocation = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypeDisciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    MaxSize_dec = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxFilesCount_num = table.Column<int>(type: "integer", nullable: true),
                    IsUploadOnly = table.Column<bool>(type: "boolean", nullable: true),
                    IsAllowVersioning = table.Column<bool>(type: "boolean", nullable: true),
                    Prefixes = table.Column<string>(type: "text", nullable: true),
                    FileTypes = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentWorkItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Actions = table.Column<string>(type: "text", nullable: true),
                    SLA_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    DMSProjectId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: true),
                    ActionUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActionRole = table.Column<string>(type: "text", nullable: true),
                    Action = table.Column<string>(type: "text", nullable: true),
                    Seq = table.Column<int>(type: "integer", nullable: true),
                    StepNo = table.Column<int>(type: "integer", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsSLABreach = table.Column<bool>(type: "boolean", nullable: false),
                    SLABreach_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentWorkItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMActivateEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMActivateEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMAddEmailGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAddEmailGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMCreateEmailGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMCreateEmailGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMCreateMailboxs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMCreateMailboxs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMDeactivateEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMDeactivateEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMDeleteEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMDeleteEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMEditEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NewEmail = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMEditEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMEditGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    NewGroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMEditGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMIncreaseMailboxSizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NewSizeInGB_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMIncreaseMailboxSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMPermissionEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPermissionEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCostItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    CostItemId = table.Column<long>(type: "bigint", nullable: true),
                    CostValue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCostItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDependants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    RelationId = table.Column<long>(type: "bigint", nullable: true),
                    FNameEn = table.Column<string>(type: "text", nullable: true),
                    MNameEn = table.Column<string>(type: "text", nullable: true),
                    LNameEn = table.Column<string>(type: "text", nullable: true),
                    FNameAr = table.Column<string>(type: "text", nullable: true),
                    MNameAr = table.Column<string>(type: "text", nullable: true),
                    LNameAr = table.Column<string>(type: "text", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDependants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    PostId = table.Column<long>(type: "bigint", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    AcquireOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Univercity = table.Column<string>(type: "text", nullable: true),
                    College = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    IsController = table.Column<bool>(type: "boolean", nullable: true),
                    IsValidProfile = table.Column<bool>(type: "boolean", nullable: true),
                    FNameEn = table.Column<string>(type: "text", nullable: true),
                    FNameAr = table.Column<string>(type: "text", nullable: true),
                    MNameEn = table.Column<string>(type: "text", nullable: true),
                    MNameAr = table.Column<string>(type: "text", nullable: true),
                    LNameEn = table.Column<string>(type: "text", nullable: true),
                    LNameAr = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PersonalEmail = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    NationalityId = table.Column<long>(type: "bigint", nullable: true),
                    MaritalId = table.Column<long>(type: "bigint", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    BloodGroupId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    AppUsrId = table.Column<long>(type: "bigint", nullable: true),
                    WorkEmail = table.Column<string>(type: "text", nullable: true),
                    DirectManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WorkShiftId = table.Column<long>(type: "bigint", nullable: true),
                    GradeId = table.Column<long>(type: "bigint", nullable: true),
                    JobId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    MedicalId = table.Column<long>(type: "bigint", nullable: true),
                    ResumeSourceId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Basic_val = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossPay_val = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    NetPay_val = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkingHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkingHoursRamadan_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    SalaryItemId = table.Column<long>(type: "bigint", nullable: true),
                    Earning_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Deduction_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMRmvEmailGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMRmvEmailGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Abbrv = table.Column<string>(type: "text", nullable: true),
                    Claims = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CAT = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Version = table.Column<decimal>(type: "numeric", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<double>(type: "double precision", nullable: true),
                    FileExtension = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    ContentBase64 = table.Column<string>(type: "text", nullable: true),
                    DownloadLink = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityId = table.Column<long>(type: "bigint", nullable: true),
                    DBFieldName = table.Column<string>(type: "text", nullable: true),
                    DataType = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityComments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    IsStartSLA = table.Column<bool>(type: "boolean", nullable: false),
                    IsResponseSLA = table.Column<bool>(type: "boolean", nullable: false),
                    IsResolveSLA = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    SLAId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUEquipmentInstalls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUEquipmentInstalls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUEquipmentMoves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUEquipmentMoves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUITAccessorieses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUITAccessorieses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUITEquipments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUITEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUMaintenances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUMaintenances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUStationaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUStationaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUWorkStationNews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUWorkStationNews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUWorkStationRemoves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUWorkStationRemoves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EUWorkStationReplaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EUWorkStationReplaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evaluation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvaluationSchemaId = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    AuditorId = table.Column<long>(type: "bigint", nullable: true),
                    ApproverId = table.Column<long>(type: "bigint", nullable: true),
                    Score = table.Column<double>(type: "double precision", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EvaluationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvaluationId = table.Column<long>(type: "bigint", nullable: true),
                    Score = table.Column<double>(type: "double precision", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSchemaItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EvaluationSchemaId = table.Column<long>(type: "bigint", nullable: true),
                    Percentage = table.Column<double>(type: "double precision", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchemaItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationSchemas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    MinScoreLimit = table.Column<double>(type: "double precision", nullable: true),
                    MaxScoreLimit = table.Column<double>(type: "double precision", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSchemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedProjects = table.Column<string>(type: "text", nullable: false),
                    PermissionLevel = table.Column<string>(type: "text", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkHelpers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cat = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkHelpers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FrameworkId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Objective = table.Column<string>(type: "text", nullable: true),
                    Principal = table.Column<string>(type: "text", nullable: true),
                    Tools = table.Column<string>(type: "text", nullable: true),
                    Guidelines = table.Column<string>(type: "text", nullable: true),
                    Deliverables = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPMaterials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPVehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DriverIDNo = table.Column<string>(type: "text", nullable: true),
                    VehicleIDNo = table.Column<string>(type: "text", nullable: true),
                    VehicleModel = table.Column<string>(type: "text", nullable: true),
                    VehicleColor = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPVehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPVisitors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    VisitorName = table.Column<string>(type: "text", nullable: true),
                    VisitorIDNo = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPVisitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRepeatedAnnually = table.Column<bool>(type: "boolean", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRBusinessCards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Count_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRBusinessCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRBusinessTrips",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Flight_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Flight_cur = table.Column<long>(type: "bigint", nullable: true),
                    Hotel_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Hotel_cur = table.Column<long>(type: "bigint", nullable: true),
                    Taxi_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Taxi_cur = table.Column<long>(type: "bigint", nullable: true),
                    Perdiem_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Perdiem_cur = table.Column<long>(type: "bigint", nullable: true),
                    TotalAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount_cur = table.Column<long>(type: "bigint", nullable: true),
                    SettlementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRBusinessTrips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRExitReentries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Days_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRExitReentries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRExpenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TotalAmount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalAmount_cur = table.Column<long>(type: "bigint", nullable: true),
                    SettlementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRLeavePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeaveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Hours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRLeavePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRLetters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ToWhom = table.Column<string>(type: "text", nullable: true),
                    IncludeSalary = table.Column<bool>(type: "boolean", nullable: true),
                    Chamber = table.Column<bool>(type: "boolean", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRLetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRLoans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TotalAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount_cur = table.Column<long>(type: "bigint", nullable: true),
                    Settled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Settled_cur = table.Column<long>(type: "bigint", nullable: true),
                    Remaining_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Remaining_cur = table.Column<long>(type: "bigint", nullable: true),
                    NumberOfMonths_num = table.Column<int>(type: "integer", nullable: false),
                    SettledMonths_num = table.Column<int>(type: "integer", nullable: false),
                    RemainingMonths_num = table.Column<int>(type: "integer", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SettlementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRLoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HROverTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Hours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HROverTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRParkings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRParkings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRRemoteWorks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRRemoteWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRResignations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LWD = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRResignations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRTrainings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingCenter = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRTrainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRVacations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRVacations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDLosses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDNo = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDLosses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDNewEmployees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDNewEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDRenewals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDNo = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDRenewals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDRestrictedAreas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDRestrictedAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Impacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCAccesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCCloudStorages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCCloudStorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCEmails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCLogins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCMonitors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCMonitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incoterms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incoterms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCOTPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ITSystemId = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCOTPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCPrinters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCPrinters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCWIFIs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCWIFIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "INCWorkstations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INCWorkstations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceAPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseOrderId = table.Column<long>(type: "bigint", nullable: true),
                    IncotermId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceAPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceARs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryAddressId = table.Column<long>(type: "bigint", nullable: true),
                    Purpose = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    SalesOrderId = table.Column<long>(type: "bigint", nullable: true),
                    IncotermId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceARs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentAPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentAPId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceAPId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_ap = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentAPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentARs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentARId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_ar = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentARs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    ITSystemId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    CriticalityId = table.Column<long>(type: "bigint", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    ActionPlan = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ITSystems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    Serial = table.Column<string>(type: "text", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PurchaseValue = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentValue = table.Column<decimal>(type: "numeric", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    GradeId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FollowUpDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: false),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    CompanyEmail = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeadSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    MarketUnitId = table.Column<long>(type: "bigint", nullable: true),
                    calc_UnitCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    ManagementBufferTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    TaxRateId = table.Column<long>(type: "bigint", nullable: true),
                    calc_Tax_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Total_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_QuantityConsumed_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_QuantityRemaining_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_QuantityBilled_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_QuantityUnBilled_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Progress_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    Term = table.Column<string>(type: "text", nullable: true),
                    BillingFrequencyId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryProofId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    ProfitCenterId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    BranchId = table.Column<long>(type: "bigint", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManagementBufferTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementBufferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maritals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maritals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    MilestoneDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsBillable = table.Column<bool>(type: "boolean", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MOMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    MOMDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Abbrv = table.Column<string>(type: "text", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objectives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objectives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ORGComplaints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGComplaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ORGSuggestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORGSuggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentAPs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_ap = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BankId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentARs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BankId = table.Column<long>(type: "bigint", nullable: true),
                    BankAccountId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentARs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTermLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Percentage = table.Column<double>(type: "double precision", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    DueDays = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTermLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PayrollId = table.Column<long>(type: "bigint", nullable: true),
                    PI_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    calc_TotalEarning_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalDeduction_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_NetPayment_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PipelineAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_GP_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_GP_prc = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pnls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pnls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortfolioManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    calc_Programs_num = table.Column<int>(type: "integer", nullable: false),
                    calc_Projects_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    HiringManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Probabilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BundleId = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDeliverables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeliverables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SKUNo = table.Column<string>(type: "text", nullable: true),
                    PN = table.Column<string>(type: "text", nullable: true),
                    CanBeSold = table.Column<bool>(type: "boolean", nullable: false),
                    CanBePurchased = table.Column<bool>(type: "boolean", nullable: false),
                    CanBeExpensed = table.Column<bool>(type: "boolean", nullable: false),
                    UOMId = table.Column<long>(type: "bigint", nullable: true),
                    DescriptionSales = table.Column<string>(type: "text", nullable: true),
                    DescriptionPurchase = table.Column<string>(type: "text", nullable: true),
                    WarningSales = table.Column<string>(type: "text", nullable: true),
                    WarningPurchase = table.Column<string>(type: "text", nullable: true),
                    UnitPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    DeliveryProofId = table.Column<long>(type: "bigint", nullable: true),
                    ProductCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    MarketUnitId = table.Column<long>(type: "bigint", nullable: true),
                    TradingTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ReOrderLimit_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    BillingFrequencyId = table.Column<long>(type: "bigint", nullable: true),
                    TaxRateId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfitCenters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    AgreementId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    calc_Projects_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectKPIs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    Month = table.Column<int>(type: "integer", nullable: true),
                    BAC = table.Column<decimal>(type: "numeric", nullable: true),
                    BCWP = table.Column<decimal>(type: "numeric", nullable: true),
                    BCWS = table.Column<decimal>(type: "numeric", nullable: true),
                    ACWP = table.Column<decimal>(type: "numeric", nullable: true),
                    SPI = table.Column<decimal>(type: "numeric", nullable: true),
                    CPI = table.Column<decimal>(type: "numeric", nullable: true),
                    ETC = table.Column<decimal>(type: "numeric", nullable: true),
                    EAC = table.Column<decimal>(type: "numeric", nullable: true),
                    Billed = table.Column<decimal>(type: "numeric", nullable: true),
                    Planned = table.Column<decimal>(type: "numeric", nullable: true),
                    Actual = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectKPIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    ShippingMethodId = table.Column<long>(type: "bigint", nullable: true),
                    RequiredDeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regulators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regulators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResumeSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    OpenDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    ImpactId = table.Column<long>(type: "bigint", nullable: true),
                    CriticalityId = table.Column<long>(type: "bigint", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    ProbabilityId = table.Column<long>(type: "bigint", nullable: true),
                    SeverityId = table.Column<long>(type: "bigint", nullable: true),
                    RiskStrategyId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: true),
                    MitigationPlan = table.Column<string>(type: "text", nullable: true),
                    ContingencyPlan = table.Column<string>(type: "text", nullable: true),
                    ITSystemId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskStrategies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskStrategies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsPermanent = table.Column<bool>(type: "boolean", nullable: false),
                    SI_val = table.Column<decimal>(type: "numeric", nullable: false),
                    SI_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    PnlId = table.Column<long>(type: "bigint", nullable: true),
                    AgreementId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTermId = table.Column<long>(type: "bigint", nullable: true),
                    ShippingMethodId = table.Column<long>(type: "bigint", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerPOId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SARemoteAccesses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SARemoteAccesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SASWLicenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SASWLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SASWServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    UrgencyId = table.Column<long>(type: "bigint", nullable: true),
                    RequestForUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ActingForUsrId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SASWServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCatalogues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: true),
                    SLAId = table.Column<long>(type: "bigint", nullable: true),
                    ServName = table.Column<string>(type: "text", nullable: true),
                    TermsConditions = table.Column<string>(type: "text", nullable: true),
                    TermsConditionsAr = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCatalogues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePriorities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Severities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FromScore = table.Column<int>(type: "integer", nullable: true),
                    ToScore = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SLAs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkShiftId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SLAServicePriorities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServicePriorityId = table.Column<long>(type: "bigint", nullable: true),
                    HolidayId = table.Column<long>(type: "bigint", nullable: true),
                    ResponseDys_num = table.Column<int>(type: "integer", nullable: false),
                    ResponseHrs_num = table.Column<int>(type: "integer", nullable: false),
                    ResponseMns_num = table.Column<int>(type: "integer", nullable: false),
                    ResolutionDys_num = table.Column<int>(type: "integer", nullable: false),
                    ResolutionHrs_num = table.Column<int>(type: "integer", nullable: false),
                    ResolutionMns_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLAServicePriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PipelineId = table.Column<long>(type: "bigint", nullable: true),
                    Pipeline_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PipelineAmount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stakeholders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    TeamMemberUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ResponsibleFor = table.Column<string>(type: "text", nullable: true),
                    AccountableFor = table.Column<string>(type: "text", nullable: true),
                    ConsultantOn = table.Column<string>(type: "text", nullable: true),
                    InformedAbout = table.Column<string>(type: "text", nullable: true),
                    Power = table.Column<double>(type: "double precision", nullable: true),
                    Interest = table.Column<double>(type: "double precision", nullable: true),
                    Support = table.Column<double>(type: "double precision", nullable: true),
                    Influence = table.Column<double>(type: "double precision", nullable: true),
                    Knowledge = table.Column<double>(type: "double precision", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stakeholders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierQuotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BidId = table.Column<long>(type: "bigint", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierQuotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OpeningBalance_val = table.Column<decimal>(type: "numeric", nullable: false),
                    IsBlock = table.Column<bool>(type: "boolean", nullable: false),
                    PriceListId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Discount_prc = table.Column<double>(type: "double precision", nullable: true),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ManagementBuffer_prc = table.Column<double>(type: "double precision", nullable: true),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_rat = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: true),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: true),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    CRNo = table.Column<string>(type: "text", nullable: true),
                    VATRegNo = table.Column<string>(type: "text", nullable: true),
                    RegistrationNo = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    SectorId = table.Column<long>(type: "bigint", nullable: true),
                    Employees_num = table.Column<int>(type: "integer", nullable: true),
                    AnnualRevenue_val = table.Column<decimal>(type: "numeric", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskAllocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAllocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskComplexities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComplexities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskPriorities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskPriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskSizes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Color = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LabelOnInvoice = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    TaxValue_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberUsrId = table.Column<long>(type: "bigint", nullable: true),
                    TeamId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamRosters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    PhaseId = table.Column<long>(type: "bigint", nullable: true),
                    MilestoneId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceARId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRosters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Domain = table.Column<string>(type: "text", nullable: true),
                    Indusrty = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    EmployeesCount = table.Column<string>(type: "text", nullable: true),
                    SubscriptionStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeAttendances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttendanceDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CheckInOut = table.Column<string>(type: "text", nullable: true),
                    CheckInOutMinutes = table.Column<double>(type: "double precision", nullable: true),
                    AssignedToId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAttendances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssignedToUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IsPending = table.Column<bool>(type: "boolean", nullable: false),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    WorkDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Progress_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    RegularHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    OverTimeHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    WeekEndHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    RegularApproved = table.Column<bool>(type: "boolean", nullable: false),
                    OverTimeApproved = table.Column<bool>(type: "boolean", nullable: false),
                    WeekEndApproved = table.Column<bool>(type: "boolean", nullable: false),
                    calc_RegularCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_OverTimeCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_WeekEndCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeZones",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UOMs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    RoundingPrecision_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Urgencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Score = table.Column<int>(type: "integer", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urgencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    Preference = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowObjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkflowId = table.Column<long>(type: "bigint", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    VueFlowExec = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowObjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityId = table.Column<long>(type: "bigint", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    VueFlowDesign = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemAssignees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkItemId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedToUsrId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemAssignees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemDependencies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PredecessorId = table.Column<long>(type: "bigint", nullable: true),
                    SuccessorId = table.Column<long>(type: "bigint", nullable: true),
                    LinkType = table.Column<string>(type: "text", nullable: true),
                    LagDays = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemDependencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsMilestone = table.Column<bool>(type: "boolean", nullable: true),
                    IsLeaf = table.Column<bool>(type: "boolean", nullable: true),
                    IsTimeSheet = table.Column<bool>(type: "boolean", nullable: true),
                    WBSCode = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    LineItemId = table.Column<long>(type: "bigint", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    PredecessorId = table.Column<long>(type: "bigint", nullable: true),
                    SuccessorId = table.Column<long>(type: "bigint", nullable: true),
                    AssignedToUsrId = table.Column<long>(type: "bigint", nullable: true),
                    PlannedEffort_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualEffort_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    PlannedCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    PlannedDuration_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualDuration_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    Analysis_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    Progress_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BaselineStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    BaselineEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActualStartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TaskSizeId = table.Column<long>(type: "bigint", nullable: true),
                    TaskPriorityId = table.Column<long>(type: "bigint", nullable: true),
                    TaskComplexityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_Comments_num = table.Column<int>(type: "integer", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AgreementId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    ProgrammeId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectManagerId = table.Column<long>(type: "bigint", nullable: true),
                    AccountManagerId = table.Column<long>(type: "bigint", nullable: true),
                    PMOAuditorId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkShiftHolidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkShiftId = table.Column<long>(type: "bigint", nullable: true),
                    HolidayId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShiftHolidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TimeZoneId = table.Column<long>(type: "bigint", nullable: true),
                    Sun_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Sun_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Mon_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Mon_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Tue_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Tue_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Wed_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Wed_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Thr_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Thr_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Fri_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Fri_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Sat_From = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Sat_To = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    No = table.Column<string>(type: "text", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "Controls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DomainId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controls_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    DealId = table.Column<long>(type: "bigint", nullable: true),
                    PnlId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Pnls_PnlId",
                        column: x => x.PnlId,
                        principalTable: "Pnls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Frameworks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DomainId = table.Column<long>(type: "bigint", nullable: true),
                    RegulatorId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frameworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frameworks_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Frameworks_Regulators_RegulatorId",
                        column: x => x.RegulatorId,
                        principalTable: "Regulators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    GenderId = table.Column<long>(type: "bigint", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    AccountManagerId = table.Column<long>(type: "bigint", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectManagerUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    TeamCount_num = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AgreementId = table.Column<long>(type: "bigint", nullable: true),
                    ProgrammeId = table.Column<long>(type: "bigint", nullable: true),
                    PortfolioId = table.Column<long>(type: "bigint", nullable: true),
                    PMOAuditorUsrId = table.Column<long>(type: "bigint", nullable: true),
                    IncrementHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxRegularHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxOverTimeHours_dec = table.Column<decimal>(type: "numeric", nullable: false),
                    Progress_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameAr = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAr = table.Column<string>(type: "text", nullable: true),
                    EntityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    EntityTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DataConfidentialityId = table.Column<long>(type: "bigint", nullable: true),
                    calc_TotalCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_TotalPrice_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_val = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount_cur = table.Column<long>(type: "bigint", nullable: true),
                    calc_Amount_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Amount_tot = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Budget_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualCost_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_val = table.Column<decimal>(type: "numeric", nullable: false),
                    CostToComplete_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_RecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnRecgnzdRevn_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Billed_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnBilled_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_DueInv_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Collected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnCollected_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Paid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPaid_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Planned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_UnPlanned_val = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_Discount_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ManagementBuffer_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_PlannedGP_prc = table.Column<decimal>(type: "numeric", nullable: false),
                    calc_ActualGP_prc = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTenants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTenants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInvitations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    TokenExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PreferredLanguage = table.Column<string>(type: "text", nullable: true),
                    PreferredCurrency = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    No = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Msg = table.Column<string>(type: "text", nullable: true),
                    ActionComment = table.Column<string>(type: "text", nullable: true),
                    PhotoUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedByUsrId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInvitations_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInvitations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInvitations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTenants_TenantId",
                table: "ApplicationUserTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTenants_UserId",
                table: "ApplicationUserTenants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SupplierId",
                table: "Contacts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Controls_DomainId",
                table: "Controls",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_DomainId",
                table: "Frameworks",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Frameworks_RegulatorId",
                table: "Frameworks",
                column: "RegulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SupplierId",
                table: "Projects",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_PnlId",
                table: "Quotes",
                column: "PnlId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvitations_RoleId",
                table: "UserInvitations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvitations_TenantId",
                table: "UserInvitations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvitations_UserId",
                table: "UserInvitations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ADAddUserGroups");

            migrationBuilder.DropTable(
                name: "ADCreateAdmnAccs");

            migrationBuilder.DropTable(
                name: "ADCreateGroups");

            migrationBuilder.DropTable(
                name: "ADCreateServAccs");

            migrationBuilder.DropTable(
                name: "ADCreateUsers");

            migrationBuilder.DropTable(
                name: "ADDisableAdmnAccs");

            migrationBuilder.DropTable(
                name: "ADDisableServAccs");

            migrationBuilder.DropTable(
                name: "ADDisableUsers");

            migrationBuilder.DropTable(
                name: "ADEnableAdmnAccs");

            migrationBuilder.DropTable(
                name: "ADEnableServAccs");

            migrationBuilder.DropTable(
                name: "ADEnableUsers");

            migrationBuilder.DropTable(
                name: "ADRmvUserGroups");

            migrationBuilder.DropTable(
                name: "ADUpdateUserInfos");

            migrationBuilder.DropTable(
                name: "ADUpdateUserMngrs");

            migrationBuilder.DropTable(
                name: "ADUpdateUserMobls");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "ApplicationUserTenants");

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
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Assumptions");

            migrationBuilder.DropTable(
                name: "AuditItemControls");

            migrationBuilder.DropTable(
                name: "AuditItemExceptions");

            migrationBuilder.DropTable(
                name: "AuditItems");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "BillingFrequencies");

            migrationBuilder.DropTable(
                name: "BillingPlans");

            migrationBuilder.DropTable(
                name: "BloodGroups");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "ChangeRequests");

            migrationBuilder.DropTable(
                name: "COCs");

            migrationBuilder.DropTable(
                name: "Competitors");

            migrationBuilder.DropTable(
                name: "Confidences");

            migrationBuilder.DropTable(
                name: "Constrains");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "Controls");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "CostItems");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "CreditNoteAPs");

            migrationBuilder.DropTable(
                name: "CreditNoteARs");

            migrationBuilder.DropTable(
                name: "Criticalities");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "CustomerPOs");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DataConfidentialities");

            migrationBuilder.DropTable(
                name: "DBLogs");

            migrationBuilder.DropTable(
                name: "DealLostReasons");

            migrationBuilder.DropTable(
                name: "Deals");

            migrationBuilder.DropTable(
                name: "DebitNoteAPs");

            migrationBuilder.DropTable(
                name: "DebitNoteARs");

            migrationBuilder.DropTable(
                name: "Deliverables");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "DeliveryNotes");

            migrationBuilder.DropTable(
                name: "DeliveryProofs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DisciplineEmployees");

            migrationBuilder.DropTable(
                name: "Disciplines");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "DMSProjectDisciplines");

            migrationBuilder.DropTable(
                name: "DMSProjectOwners");

            migrationBuilder.DropTable(
                name: "DMSProjects");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "DocumentTypeDisciplines");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "DocumentWorkItems");

            migrationBuilder.DropTable(
                name: "EMActivateEmails");

            migrationBuilder.DropTable(
                name: "EMAddEmailGroups");

            migrationBuilder.DropTable(
                name: "EMCreateEmailGroups");

            migrationBuilder.DropTable(
                name: "EMCreateMailboxs");

            migrationBuilder.DropTable(
                name: "EMDeactivateEmails");

            migrationBuilder.DropTable(
                name: "EMDeleteEmails");

            migrationBuilder.DropTable(
                name: "EMEditEmails");

            migrationBuilder.DropTable(
                name: "EMEditGroups");

            migrationBuilder.DropTable(
                name: "EMIncreaseMailboxSizes");

            migrationBuilder.DropTable(
                name: "EMPermissionEmails");

            migrationBuilder.DropTable(
                name: "EmployeeCostItems");

            migrationBuilder.DropTable(
                name: "EmployeeDependants");

            migrationBuilder.DropTable(
                name: "EmployeePosts");

            migrationBuilder.DropTable(
                name: "EmployeeQualifications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryItems");

            migrationBuilder.DropTable(
                name: "EMRmvEmailGroups");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "EntityAttachments");

            migrationBuilder.DropTable(
                name: "EntityAttributes");

            migrationBuilder.DropTable(
                name: "EntityComments");

            migrationBuilder.DropTable(
                name: "EntityStatuses");

            migrationBuilder.DropTable(
                name: "EntityTypes");

            migrationBuilder.DropTable(
                name: "EUEquipmentInstalls");

            migrationBuilder.DropTable(
                name: "EUEquipmentMoves");

            migrationBuilder.DropTable(
                name: "EUITAccessorieses");

            migrationBuilder.DropTable(
                name: "EUITEquipments");

            migrationBuilder.DropTable(
                name: "EUMaintenances");

            migrationBuilder.DropTable(
                name: "EUStationaries");

            migrationBuilder.DropTable(
                name: "EUWorkStationNews");

            migrationBuilder.DropTable(
                name: "EUWorkStationRemoves");

            migrationBuilder.DropTable(
                name: "EUWorkStationReplaces");

            migrationBuilder.DropTable(
                name: "Evaluation");

            migrationBuilder.DropTable(
                name: "EvaluationItems");

            migrationBuilder.DropTable(
                name: "EvaluationSchemaItems");

            migrationBuilder.DropTable(
                name: "EvaluationSchemas");

            migrationBuilder.DropTable(
                name: "ExternalUsers");

            migrationBuilder.DropTable(
                name: "FrameworkHelpers");

            migrationBuilder.DropTable(
                name: "FrameworkItems");

            migrationBuilder.DropTable(
                name: "Frameworks");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "GPMaterials");

            migrationBuilder.DropTable(
                name: "GPVehicles");

            migrationBuilder.DropTable(
                name: "GPVisitors");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "HRBusinessCards");

            migrationBuilder.DropTable(
                name: "HRBusinessTrips");

            migrationBuilder.DropTable(
                name: "HRExitReentries");

            migrationBuilder.DropTable(
                name: "HRExpenses");

            migrationBuilder.DropTable(
                name: "HRLeavePermissions");

            migrationBuilder.DropTable(
                name: "HRLetters");

            migrationBuilder.DropTable(
                name: "HRLoans");

            migrationBuilder.DropTable(
                name: "HROverTimes");

            migrationBuilder.DropTable(
                name: "HRParkings");

            migrationBuilder.DropTable(
                name: "HRRemoteWorks");

            migrationBuilder.DropTable(
                name: "HRResignations");

            migrationBuilder.DropTable(
                name: "HRTrainings");

            migrationBuilder.DropTable(
                name: "HRVacations");

            migrationBuilder.DropTable(
                name: "IDLosses");

            migrationBuilder.DropTable(
                name: "IDNewEmployees");

            migrationBuilder.DropTable(
                name: "IDRenewals");

            migrationBuilder.DropTable(
                name: "IDRestrictedAreas");

            migrationBuilder.DropTable(
                name: "Impacts");

            migrationBuilder.DropTable(
                name: "INCAccesses");

            migrationBuilder.DropTable(
                name: "INCAccounts");

            migrationBuilder.DropTable(
                name: "INCCloudStorages");

            migrationBuilder.DropTable(
                name: "INCEmails");

            migrationBuilder.DropTable(
                name: "INCLogins");

            migrationBuilder.DropTable(
                name: "INCMonitors");

            migrationBuilder.DropTable(
                name: "Incoterms");

            migrationBuilder.DropTable(
                name: "INCOTPs");

            migrationBuilder.DropTable(
                name: "INCPrinters");

            migrationBuilder.DropTable(
                name: "INCWIFIs");

            migrationBuilder.DropTable(
                name: "INCWorkstations");

            migrationBuilder.DropTable(
                name: "Industries");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "InvoiceAPs");

            migrationBuilder.DropTable(
                name: "InvoiceARs");

            migrationBuilder.DropTable(
                name: "InvoicePaymentAPs");

            migrationBuilder.DropTable(
                name: "InvoicePaymentARs");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "ITSystems");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "LeadSources");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "LineItems");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "ManagementBufferTypes");

            migrationBuilder.DropTable(
                name: "Maritals");

            migrationBuilder.DropTable(
                name: "MarketUnits");

            migrationBuilder.DropTable(
                name: "Medicals");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "MOMs");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "Objectives");

            migrationBuilder.DropTable(
                name: "ORGComplaints");

            migrationBuilder.DropTable(
                name: "ORGSuggestions");

            migrationBuilder.DropTable(
                name: "PaymentAPs");

            migrationBuilder.DropTable(
                name: "PaymentARs");

            migrationBuilder.DropTable(
                name: "PaymentTermLines");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropTable(
                name: "PayrollItems");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "Pipelines");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Probabilities");

            migrationBuilder.DropTable(
                name: "ProductActivities");

            migrationBuilder.DropTable(
                name: "ProductBundles");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductDeliverables");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProfitCenters");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "ProjectKPIs");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "ResumeSources");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "RiskStrategies");

            migrationBuilder.DropTable(
                name: "SalaryItems");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "SARemoteAccesses");

            migrationBuilder.DropTable(
                name: "SASWLicenses");

            migrationBuilder.DropTable(
                name: "SASWServices");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "ServiceCatalogues");

            migrationBuilder.DropTable(
                name: "ServicePriorities");

            migrationBuilder.DropTable(
                name: "Severities");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "SLAs");

            migrationBuilder.DropTable(
                name: "SLAServicePriorities");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Stakeholders");

            migrationBuilder.DropTable(
                name: "SupplierQuotes");

            migrationBuilder.DropTable(
                name: "TaskAllocations");

            migrationBuilder.DropTable(
                name: "TaskComplexities");

            migrationBuilder.DropTable(
                name: "TaskPriorities");

            migrationBuilder.DropTable(
                name: "TaskSizes");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TeamRosters");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "TimeAttendances");

            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "TimeZones");

            migrationBuilder.DropTable(
                name: "TradingTypes");

            migrationBuilder.DropTable(
                name: "UOMs");

            migrationBuilder.DropTable(
                name: "Urgencies");

            migrationBuilder.DropTable(
                name: "UserInvitations");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "WorkflowObjects");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "WorkItemAssignees");

            migrationBuilder.DropTable(
                name: "WorkItemDependencies");

            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "WorkShiftHolidays");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "Regulators");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Pnls");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
