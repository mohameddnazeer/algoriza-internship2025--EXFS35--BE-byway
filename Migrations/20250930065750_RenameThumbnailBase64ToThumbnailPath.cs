using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Byway.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameThumbnailBase64ToThumbnailPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailBase64",
                table: "Courses",
                newName: "ThumbnailPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailPath",
                table: "Courses",
                newName: "ThumbnailBase64");
        }
    }
}
