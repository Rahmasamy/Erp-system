using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Aspire.Data.Migrations
{
    /// <inheritdoc />
    public partial class OldWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedProjects",
                table: "Contractors");

            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Contractors",
                newName: "OldKey");

            migrationBuilder.AddColumn<string>(
                name: "OldKey",
                table: "DMSProjectOwners",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OldWorkflows",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Steps = table.Column<string>(type: "text", nullable: true),
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
                    OldKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OldWorkflows", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OldWorkflows");

            migrationBuilder.DropColumn(
                name: "OldKey",
                table: "DMSProjectOwners");

            migrationBuilder.RenameColumn(
                name: "OldKey",
                table: "Contractors",
                newName: "OldId");

            migrationBuilder.AddColumn<string>(
                name: "AssignedProjects",
                table: "Contractors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
