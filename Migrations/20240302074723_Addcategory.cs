using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveTyingGameBlazor.Migrations
{
    /// <inheritdoc />
    public partial class Addcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "RegisteredVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "RegisteredVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "RegisteredVideos");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "RegisteredVideos");
        }
    }
}
