using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSketch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverImageCaptionField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageCaption",
                table: "Articles",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageCaption",
                table: "Articles");
        }
    }
}
