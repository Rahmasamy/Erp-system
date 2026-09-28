using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspire.Data.Migrations
{
    /// <inheritdoc />
    public partial class oldkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "ExternalUsers",
                newName: "OldKey");

            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "DocumentTypes",
                newName: "OldKey");

            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Documents",
                newName: "OldKey");

            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "DMSProjects",
                newName: "OldKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OldKey",
                table: "ExternalUsers",
                newName: "OldId");

            migrationBuilder.RenameColumn(
                name: "OldKey",
                table: "DocumentTypes",
                newName: "OldId");

            migrationBuilder.RenameColumn(
                name: "OldKey",
                table: "Documents",
                newName: "OldId");

            migrationBuilder.RenameColumn(
                name: "OldKey",
                table: "DMSProjects",
                newName: "OldId");
        }
    }
}
